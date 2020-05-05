using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class GraphEdgeData {
	public readonly int v0;
	public readonly int v1;

	public GraphEdgeData (int i, int j) {
		if (i < j) {
			v0 = i;
			v1 = j;
		} else {
			v0 = j;
			v1 = i;
		} // v0 is less than v1
	}

	public override bool Equals (object other) {
		if (!(other is GraphEdgeData))
			return false;
		return Equals ((GraphEdgeData)other);
	}

	public bool Equals (GraphEdgeData other) {
		return (v0 == other.v0) && (v1 == other.v1);
	}

	public override int GetHashCode () {
		return v0 * 100 + v1;
	}
}

public class TestExec : MonoBehaviour {
	int m_CommitCount;
	const int CommitMax = 3;
	const int CommitDone = CommitMax + 1;

	InputField[] m_InputFields;
	Button m_BtnExec;
	Text m_TxtResult;

	HashSet<GraphEdgeData> m_Edges;

	// Use this for initialization
	void Start () {
		Button[] buttons = GetComponentsInChildren<Button> ();
		foreach (var item in buttons) {
			if (item.tag == "my_tag_1") {
				m_BtnExec = item;
				m_BtnExec = GetComponentInChildren<Button> ();
				m_BtnExec.onClick.AddListener (this.OnClickedExecute);
				continue;
			}
			if (item.tag == "my_tag_2") {
				item.onClick.AddListener (this.OnClickedCommit);
				continue;
			}
		}

		//
		m_InputFields = GetComponentsInChildren<InputField> ();

		//
		Text[] texts = GetComponentsInChildren<Text> ();
		foreach (var item in texts) {
			if (item.CompareTag ("my_tag_2")) {
				m_TxtResult = item;
				break;
			}
		}

		//
		m_Edges = new HashSet<GraphEdgeData> ();
		m_Edges.Add (new GraphEdgeData (0, 1));
		m_Edges.Add (new GraphEdgeData (0, 3));
		m_Edges.Add (new GraphEdgeData (0, 4));
		m_Edges.Add (new GraphEdgeData (1, 2));
		m_Edges.Add (new GraphEdgeData (1, 4));
		m_Edges.Add (new GraphEdgeData (2, 3));
		m_Edges.Add (new GraphEdgeData (3, 4));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClickedExecute () {
		if (ValidateAnswer ()) {
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			sb.AppendLine ("0 --- 4 0 --- 3 0 --- 1");
			sb.AppendLine ("1 --- 4 1 --- 2 1 --- 0");
			sb.AppendLine ("2 --- 3 2 --- 1");
			sb.AppendLine ("3 --- 4 3 --- 2 3 --- 0");
			sb.AppendLine ("4 --- 3 4 --- 1 4 --- 0");
			ShowFeedbackText (sb.ToString (), false);
			m_BtnExec.gameObject.SetActive (false);
			DisableInput ();
			return;
		}

		m_CommitCount++;
		if (m_CommitCount > CommitMax) {
			ShowFeedbackText ("填写错误超过3次，已给出正确答案。");
			ShowAnswer ();
			DisableInput ();
		}
	}

	bool ValidateAnswer () {
		HashSet<GraphEdgeData> goodInputs = new HashSet<GraphEdgeData> ();
		HashSet<InputField> badFormats = new HashSet<InputField> ();
		HashSet<InputField> wrongInputs = new HashSet<InputField> ();

		foreach (var item in m_InputFields) {
			string input = item.text.Trim ();
			string[] vertices = input.Split (' ');
			if (vertices.Length != 2) {
				badFormats.Add (item);
				continue;
			}
			int v0, v1;
			if (!int.TryParse (vertices [0], out v0)) {
				badFormats.Add (item);
				continue;
			}
			if (!int.TryParse (vertices [1], out v1)) {
				badFormats.Add (item);
				continue;
			}

			GraphEdgeData userEdge = new GraphEdgeData (v0, v1);
			if (!m_Edges.Contains (userEdge)) {
				wrongInputs.Add (item);
				continue;
			}
			if (goodInputs.Contains (userEdge)) {
				wrongInputs.Add (item); // duplicate input
				continue;
			}
			goodInputs.Add (userEdge);
		}

		// reset
		foreach (var item in m_InputFields) {
			HighlightInput (item, false);
		}

		if (badFormats.Count > 0) {
			// highlight them
			foreach (var item in badFormats) {
				HighlightInput (item);
			}
			ShowFeedbackText ("请按格式要求填写每条边，已标记错误项。");
			return false;
		}
		if (wrongInputs.Count > 0) {
			// highlight them
			foreach (var item in wrongInputs) {
				HighlightInput (item);
			}
			ShowFeedbackText ("填写有错误，已标记错误项。");
			return false;
		}
		return true;
	}

	void HighlightInput (InputField input, bool highlighted = true) {
		Image image = input.GetComponent<Image> ();
		Color bg = new Color (image.color.r, image.color.g, image.color.b, highlighted ? 1.0F : 0.0F);
		image.color = bg;
	}

	void ShowFeedbackText (string msg, bool highlighted = true) {
		if (highlighted) {
			m_TxtResult.text = string.Format ("<color=red>{0}</color>", msg);
		} else {
			m_TxtResult.text = msg;
		}
	}

	void ShowAnswer () {
		GraphEdgeData[] answers = new GraphEdgeData[m_Edges.Count];
		m_Edges.CopyTo (answers);
		for (int i = 0; i < answers.Length; i++) {
			m_InputFields [i].text = string.Format ("{0} {1}", answers [i].v0, answers [i].v1);
			HighlightInput (m_InputFields [i], false);
		}
	}

	void OnClickedCommit () {
		if (m_BtnExec.IsActive ()) {
			MyDialog.Instance.ShowMsgBox ("请先完成无向图的学习");
			return;
		}
		MyDialog.Instance.ShowMsgBox ("继续学习有向图的邻接表结构");
		//MyDialog.Instance.ButtonAction = () =>
	}

	void DisableInput () {
		foreach (var item in m_InputFields) {
			item.readOnly = true;
		}
	}
}
