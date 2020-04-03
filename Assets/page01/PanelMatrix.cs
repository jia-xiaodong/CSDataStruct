using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMatrix : MonoBehaviour {
	QuizGroupHelp m_GroupHelp;

	// Use this for initialization
	void Start () {
		Button[] buttons = GetComponentsInChildren<Button> ();
		foreach (var btn in buttons) {
			if (btn.name.CompareTo ("btn_reset") == 0) {
				btn.onClick.AddListener (OnResetClick);
			} else if (btn.name.CompareTo ("btn_commit") == 0) {
				btn.onClick.AddListener (OnCommitClick);
			}
		}

		m_GroupHelp = Parent.GetComponentInChildren<QuizGroupHelp> ();
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

	private void OnCommitClick() {
		bool errorFound = false;
		InputField[] inputs = Parent.GetComponentsInChildren<InputField> (); // "inputs" are all active InputFields on canvas.
		foreach (var i in inputs) {
			QuizHelp help = i.GetComponent<QuizHelp> ();
			if (help == null)
				continue;
			if (!help.Validate ()) {
				errorFound = true;
			}
		}
		if (!m_GroupHelp.Validate ()) {
			errorFound = true;
		}

		MapType current = GetMapType ();
		MapType another = (current == MapType.MapBeijing ? MapType.MapChina : MapType.MapBeijing);
		// Test is finished good.
		if (!errorFound) {
			TestStats.Instance.SetTestDone (current, true);
			if (TestStats.Instance.IsTestDone (another)) { // Both tests are done...
				TestStats.Instance.MessageText = "顶点与边的集合学习完毕，进入下一阶段的学习";
				//TestStats.Instance.ButtonAction = /* jump to next page */
				TestStats.Instance.ShowMessageBox ();
			} else {
				string message1 = "有向图填写完毕，继续学习无向图";
				string message2 = "无向图填写完毕，继续学习有向图";
				TestStats.Instance.MessageText = (current == MapType.MapChina ? message1 : message2);
				TestStats.Instance.ButtonAction = () => MapTab.Instance.ShowMap (another);
				TestStats.Instance.ShowMessageBox ();
			}
		// Test fails.
		} else {
			TestStats.Instance.IncErrorCount (current);
			if (TestStats.Instance.GetErrorCount (current) > TestStats.ErrorMax) {
				TestStats.Instance.MessageText = "超过三次填写错误，已给出正确答案";
				TestStats.Instance.ShowMessageBox ();
				//
				foreach (var i in inputs) {
					QuizHelp help = i.GetComponent<QuizHelp> ();
					if (help == null)
						continue;
					help.ShowGoodAnswer ();
				}
				m_GroupHelp.ShowGoodAnswer ();
			} else {
				TestStats.Instance.MessageText = "填写错误，已标记错误项";
				TestStats.Instance.ShowMessageBox ();
			}
		}
	}

	// parent game object is "GeoGraph"
	private GameObject Parent {
		get { return transform.parent.gameObject; }
	}

	public bool IsChinaMap {
		get {
			GeoGraph map = Parent.GetComponent<GeoGraph> ();
			return map.IsChinaMap;
		}
	}

	public MapType GetMapType () {
		return IsChinaMap ? MapType.MapChina : MapType.MapBeijing;
	}
}
