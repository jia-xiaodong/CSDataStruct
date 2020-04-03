using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PathNode {
	public GraphNode node;
	public int distance;
	public int previous;

	public int index {
		get { return node.Index; }
	}
}

public class GeoGraph : MonoBehaviour {
	const int MAX_WEIGHT = 999;
	private List<GraphNode> m_NodeTable;
	private List<GraphEdge> m_EdgeTable;
	private int[,] m_AdjacencyMatrix;
	Queue<GraphNode> m_WayPoints;
	GraphNode m_NextWayPoint;
	Plane m_Plane;
	public bool m_IsEdgeDirected;  // Directed Graph vs. Non-directed Graph?

	// Use this for initialization
	void Start () {
		GraphNode[] nodes = this.GetComponentsInChildren<GraphNode> ();
		m_NodeTable = new List<GraphNode> (nodes);
		GraphEdge[] edges = this.GetComponentsInChildren<GraphEdge> ();
		m_EdgeTable = new List<GraphEdge> (edges);
		// associate label before sorting the nodes according to their labels
		m_NodeTable.ForEach (n => n.AssociateLabel ());
		m_NodeTable.Sort ((n1, n2) => string.Compare (n1.Label, n2.Label));
		for (int i = 0; i < m_NodeTable.Count; i++) {
			m_NodeTable [i].Index = i;  // this "Index" is used in Adjacency Matrix
		}
		//
		m_WayPoints = new Queue<GraphNode> ();
		//
		BuildAdjacencyMatrix ();
		//
		InitializePlane();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Plane.Landed()) {
			m_NextWayPoint.Visited = true;
			PlaneNextWayPoint ();
			m_Plane.SetNewFlight (m_NextWayPoint);
		}
	}

	//! Graph needs an "Ajacency Matrix" to judge connection between nodes rapidly.
	void BuildAdjacencyMatrix () {
		// initialize adjacency matrix
		m_AdjacencyMatrix = new int[m_NodeTable.Count, m_NodeTable.Count];  // default value: 0
		for (int i = 0; i < m_NodeTable.Count; i++) {
			for (int j = 0; j < m_NodeTable.Count; j++) {
				m_AdjacencyMatrix [i, j] = (i == j ? 0 : MAX_WEIGHT);
			}
		}
		// traverse edges
		HashSet<int> connected = new HashSet<int> ();
		foreach (GraphEdge edge in m_EdgeTable) {
			int i = edge.StartNode.Index;
			int j = edge.StopNode.Index;
			connected.Add (i);
			connected.Add (j);
			m_AdjacencyMatrix [i, j] = 1;
			if (!m_IsEdgeDirected)
				m_AdjacencyMatrix [j, i] = 1;
			Debug.Log ("edge (" + m_NodeTable[i].Label + "->" + m_NodeTable[j].Label + ")");
		}
		if (connected.Count < m_NodeTable.Count)
			Debug.LogError ("[jxd] Dangling node is found!");
	}

	List<PathNode> Dijkstra (int start) {
		List<PathNode> good = new List<PathNode> ();
		LinkedList<PathNode> bad = new LinkedList<PathNode> ();
		// 1. init
		foreach (GraphNode v in m_NodeTable) {
			if (v.Index == start)
				continue;
			PathNode n = new PathNode ();
			n.node = v;
			n.distance = m_AdjacencyMatrix [start, v.Index];
			n.previous = start;
			bad.AddLast (n);
		}
		// 2.
		int min;
		PathNode shortest = null;
		while (bad.Count > 0) {
			min = MAX_WEIGHT;
			// find a node that has shortest distance
			foreach (PathNode n in bad) {
				if (n.distance < min) {
					shortest = n;
				}
			}
			//
			good.Add (shortest);
			bad.Remove (shortest);
			//
			foreach (PathNode n in bad) {
				int w = m_AdjacencyMatrix [shortest.index, n.index];
				if (w == MAX_WEIGHT)
					continue;
				if (shortest.distance + w < n.distance) {
					n.distance = shortest.distance + w;
					n.previous = shortest.index;
				}
			}
		}
		return good;
	}

	void PlanShortestPath (int start, int stop) {
		List<PathNode> paths = Dijkstra (start);
		Stack<int> path = new Stack<int> ();
		// put path nodes to a stack (from stop to start)
		int target = stop;
		while (target != start) {
			PathNode node = paths.Find (n => n.index == target);
			path.Push (node.index);
			target = node.previous;
		}
		// reverse path nodes to normal order (from start to stop)
		while (path.Count > 0) {
			int i = path.Pop ();
			m_WayPoints.Enqueue (m_NodeTable [i]);
		}
	}

	void InitializePlane () {
		const int start = 0;
		const int first_stop = 1;

		ClearNodesMark ();
		PlanShortestPath (start, first_stop);
		m_NextWayPoint = m_WayPoints.Dequeue (); // flight line (start --> first_stop) is divided to several way points

		m_Plane = GetComponentInChildren<Plane> ();
		m_Plane.AimTo (m_NodeTable[start], m_NextWayPoint);
	}

	void PlaneNextWayPoint () {
		// not finish full-path
		if (m_WayPoints.Count > 0) {
			m_NextWayPoint = m_WayPoints.Dequeue ();
			return;
		}

		// find another unvisited node
		foreach (GraphNode n in m_NodeTable) {
			if (n.Visited) {
				continue;
			}
			PlanShortestPath (m_NextWayPoint.Index, n.Index);
			m_NextWayPoint = m_WayPoints.Dequeue ();
			return;
		}

		// if all nodes are visited, restart another round of traverse
		ClearNodesMark ();
		PlanShortestPath (m_NextWayPoint.Index, (m_NextWayPoint.Index+1) % m_NodeTable.Count);
		m_NextWayPoint = m_WayPoints.Dequeue ();
	}

	// before every full-map traverse, clear all "visited" marks.
	void ClearNodesMark () {
		m_NodeTable.ForEach (n => n.Visited = false);
	}

	public void OnTabChanged (object sender, TabChangedEventArgs e) {
		MapType thisGraph = MapType.MapBeijing;
		if (string.Compare (gameObject.tag, "china", System.StringComparison.OrdinalIgnoreCase) == 0) {
			thisGraph = MapType.MapChina;
		}

		bool isActive = (thisGraph == e.currentTab);
		if (gameObject.activeSelf != isActive)
			gameObject.SetActive (isActive);
	}

	public bool IsChinaMap {
		get { return m_IsEdgeDirected; }
	}
}
