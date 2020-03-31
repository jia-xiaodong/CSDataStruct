using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphNode : MonoBehaviour
{
	Text m_Text;
	bool m_visited;
	int m_Index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	void OnDrawGizmos () {
		// Both methods work well.

		// 1. simpler
		//Gizmos.color = Color.red;
		//Gizmos.DrawSphere(transform.position, 5);

		// 2. make sure you tick off the checkbox "3D icons" in Gizmos popup menu.
		// So that it can show off in this 2D application.
		Gizmos.DrawIcon(transform.position, "GraphNode.png", true);
	}
	*/

	public void AssociateLabel () {
		m_Text = this.GetComponentInChildren<Text> ();
	}

	public string Label
	{
		get { return m_Text.text; }
	}

	public int Index {
		get { return m_Index; }
		set { m_Index = value; }
	}

	// to which graph this node is belonging.
	public GeoGraph Parent {
		get {
			// todo: which one is correct?
			GeoGraph parent1 = this.transform.parent.GetComponent<GeoGraph> ();
			GeoGraph parent2 = this.transform.parent.gameObject.GetComponent<GeoGraph> ();
			return parent1;
		}
	}

	public bool Visited {
		get { return m_visited; }
		set { m_visited = value; }
	}
}
