using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEdge : MonoBehaviour {
	public GraphNode m_StartNode;
	public GraphNode m_StopNode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// is it necessary? No.
	/*
	void OnDrawGizmos () {
		if (m_StartNode == null || m_StopNode == null)
			return;
		Gizmos.color = Color.red;
		Gizmos.DrawLine (m_StartNode.transform.position, m_StopNode.transform.position);
	}
	*/

	public GraphNode StartNode {
		get { return m_StartNode; }
		set { m_StartNode = value; }
	}

	public GraphNode StopNode {
		get { return m_StopNode; }
		set { m_StopNode = value; }
	}

	public void PlaceArrow () {
		if (!this.StartNode || !this.StopNode)
			return;
		// 1. translate
		Vector3 pos1 = this.StartNode.transform.position;
		Vector3 pos2 = this.StopNode.transform.position;
		this.transform.position = (pos1 + pos2) / 2;
		// 2. rotate
		//   "Vector3.left" is the initial direction-vector of arrow.
		//   "pos2-pos1" is the expected direction-vector of arrow.
		this.transform.rotation = Quaternion.FromToRotation (Vector3.left, pos2-pos1);
		// 3. scale
		RectTransform rt = this.GetComponent<RectTransform> ();
		float h = rt.rect.height;
		float w = Vector2.Distance (new Vector2 (pos1.x, pos1.y), new Vector2 (pos2.x, pos2.y));
		rt.sizeDelta = new Vector2 (w-12, h); // subtract a few pixels because GraphNode has radius.
	}
}
