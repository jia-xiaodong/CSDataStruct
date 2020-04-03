using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNodeEdge : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Button reset = GetComponentInChildren<Button> ();
		reset.onClick.AddListener (OnResetClick);

		QuizGroupHelp quizGroup = GetComponentInChildren<QuizGroupHelp> ();

		InputField[] inputs = GetComponentsInChildren<InputField> ();
		List<InputField> cache = new List<InputField> ();
		foreach (var i in inputs) {
			if (string.CompareOrdinal (i.name, "InputField-V") == 0) { // it needs a special validator.
				QuizHelp help = i.GetComponent<QuizHelp> ();
				if (help != null)
					help.Validator = NodeSetValidator;
				continue;
			}

			// by matching names, find a pair of graph nodes for one edge.
			if (i.name.StartsWith ("InputField-E")) {
				int pos = "InputField-E".Length;
				char group = i.name [pos];
				InputField match = cache.Find(input => input.name[pos] == group);
				if (match == null) {
					cache.Add (i);
				} else {
					char order1 = i.name [pos + 1];
					char order2 = match.name [pos + 1];
					if (order1 < order2) {
						quizGroup.AddEdgeInput (i, match);
					} else {
						quizGroup.AddEdgeInput (match, i);
					}
					cache.Remove (match);
				}
				continue;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnResetClick() {
		InputField[] inputs = GetComponentsInChildren<InputField> ();
		foreach (var i in inputs) {
			i.text = "";
		}
	}

	public bool IsChinaMap {
		get {
			GameObject parent = transform.parent.gameObject;
			GeoGraph map = parent.GetComponent<GeoGraph> ();
			return map.IsChinaMap;
		}
	}

	protected static bool NodeSetValidator (string userAnswer, string goodAnswer) {
		HashSet<char> userCharSet = new HashSet<char> (userAnswer.ToLower ());
		HashSet<char> goodCharSet = new HashSet<char> (goodAnswer.ToLower ());
		return userCharSet.SetEquals (goodCharSet);
	}
}
