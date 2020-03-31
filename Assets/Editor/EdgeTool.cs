using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EdgeTool : ScriptableObject {
	static GraphEdge s_TargetEdge;

	[MenuItem("Jxd/Page01/Set Target Edge")]
	static void SetTargetEdge () {
		GraphEdge edge = GetSelectedGraphEdge ();
		if (edge == null) {
			Debug.LogError ("[jxd] Edge isn't selected!");
			return;
		}
		s_TargetEdge = edge;
	}

	[MenuItem("Jxd/Page01/Set Start Node")]
	static void SetStartNode() {
		if (s_TargetEdge == null) {
			Debug.LogError ("[jxd] Please select an edge at first!");
			return;
		}
		GraphNode old = s_TargetEdge.StartNode;
		s_TargetEdge.StartNode = GetSelectedGraphNode ();
		s_TargetEdge.PlaceArrow ();
		//! make sure to mark scene dirty proactively
		if (old != s_TargetEdge.StartNode) {
			EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
		}
	}

	[MenuItem("Jxd/Page01/Set Stop Node")]
	static void SetStopNode() {
		if (s_TargetEdge == null) {
			Debug.LogError ("[jxd] Please select an edge at first!");
			return;
		}
		GraphNode old = s_TargetEdge.StopNode;
		s_TargetEdge.StopNode = GetSelectedGraphNode ();
		s_TargetEdge.PlaceArrow ();
		//! make sure to mark scene dirty proactively
		if (old != s_TargetEdge.StopNode) {
			EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
		}
	}

	static GraphEdge GetSelectedGraphEdge () {
		if (!Selection.activeGameObject)
			return null;
		if (Selection.GetTransforms (SelectionMode.Unfiltered).Length > 1)
			return null;
		if (Selection.activeGameObject.tag.CompareTo ("graph_edge") != 0)
			return null;
		GraphEdge edge = Selection.activeGameObject.GetComponent<GraphEdge> ();
		Debug.Log ("[jxd] selected: " + edge.gameObject.name);
		return edge;
	}

	static GraphNode GetSelectedGraphNode () {
		if (!Selection.activeGameObject)
			return null;
		if (Selection.GetTransforms (SelectionMode.Unfiltered).Length > 1)
			return null;
		if (Selection.activeGameObject.tag.CompareTo ("graph_node") != 0)
			return null;
		GraphNode node = Selection.activeGameObject.GetComponent<GraphNode> ();
		Debug.Log ("[jxd] selected: " + node.gameObject.name);
		return node;
	}
}
