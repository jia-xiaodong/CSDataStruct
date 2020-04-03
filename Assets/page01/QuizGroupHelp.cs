using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Unity seems not to support Tuple. So I made one by myself.
class EdgeInput {
	public InputField input1;
	public InputField input2;

	public EdgeInput (InputField i1, InputField i2)
	{
		input1 = i1;
		input2 = i2;
	}

	/*
	 * @param orderMatters: when order matters, it indicates it's a directed graph.
	 */
	public bool Equals (InputField i1, InputField i2, bool orderMatters = true) {
		bool same = (input1 == i1 && input2 == i2);
		if (same)
			return true;
		return orderMatters ? false : (input1 == i2 && input2 == i1);
	}

	/*
	 * @param orderMatters: when order doesn't matter, it indicates it's a non-directed graph.
	 */
	public bool TextEquals (string node1, string node2, bool orderMatters = true) {
		string n1 = input1.text.ToLower ();
		string n2 = input2.text.ToLower ();
		string n12 = node1.ToLower ();
		string n22 = node2.ToLower ();
		bool same = (string.Compare (n12, n1) == 0 && string.Compare (n22, n2) == 0);
		if (same)
			return true;
		if (orderMatters) {
			return false;
		} else {
			same = (string.Compare (n12, n2) == 0 && string.Compare (n22, n1) == 0);
			return same;
		}
	}

	public void ShowColoredInput (bool correct) {
		Image image = input1.GetComponent<Image> ();
		image.color = correct ? Color.white : Color.red;
		image = input2.GetComponent<Image> ();
		image.color = correct ? Color.white : Color.red;
	}
}

/* 
 * 
 * 
 * 
 */
public class QuizGroupHelp : MonoBehaviour {
	public string m_Answer;
	List<EdgeInput> m_Inputs;

	// Use this for initialization
	void Start () {
		m_Inputs = new List<EdgeInput> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	 * @param input1: represent the 1st graph node
	 * @param input2: represent the 2nd graph node
	 */
	public void AddEdgeInput (InputField input1, InputField input2) {
		if (m_Inputs.Find (n => n.Equals(input1, input2, IsChinaMap)) != null)
			return;
		m_Inputs.Add (new EdgeInput (input1, input2));
	}

	public bool IsChinaMap {
		get {
			GameObject greatParent = transform.parent.parent.gameObject;
			GeoGraph map = greatParent.GetComponent<GeoGraph> ();
			return map.IsChinaMap;
		}
	}

	/* Check every InputField.
	 * @return true if no error is found.
	 */
	public bool Validate () {
		HashSet<EdgeInput> goodInputs = new HashSet<EdgeInput> ();
		foreach (var a in GoodAnswer) {
			EdgeInput ei = m_Inputs.Find (e => e.TextEquals (a.Substring (0, 1), a.Substring (1, 1), IsChinaMap));
			if (ei != null) {
				goodInputs.Add (ei);
			}
		}
		HashSet<EdgeInput> badInputs = new HashSet<EdgeInput> (m_Inputs);
		badInputs.ExceptWith (goodInputs);
		foreach (var i in badInputs) {
			i.ShowColoredInput (false);
		}
		foreach (var i in goodInputs) {
			i.ShowColoredInput (true);
		}
		return badInputs.Count == 0;
	}

	public void ShowGoodAnswer () {
		List<string> goodAnswers = new List<string> (GoodAnswer);
		if (goodAnswers.Count != m_Inputs.Count)
			return;
		for (int i = 0; i < goodAnswers.Count; i++) {
			string answer = goodAnswers [i];
			EdgeInput ei = m_Inputs [i];
			ei.input1.text = answer.Substring (0, 1);
			ei.input2.text = answer.Substring (1, 1);
			ei.ShowColoredInput (true);
		}
	}

	protected HashSet<string> GoodAnswer {
		get { return new HashSet<string> (m_Answer.Split (new char[]{ ',', ' ' })); }
	}
}
