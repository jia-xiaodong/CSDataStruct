using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestProc : MonoBehaviour {
	int m_CommitCount;
	InputField[] m_InputFields;
	const int CommitMax = 3;

	// Use this for initialization
	void Start () {
		Button commit = GetComponentInChildren<Button> ();
		if (commit != null) {
			commit.onClick.AddListener (this.OnCommitClicked);
		}
		m_InputFields = GetComponentsInChildren<InputField> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCommitClicked () {
		if (m_CommitCount > CommitMax) {
			MultiTabbedPanel tabControl = GetComponentInParent<MultiTabbedPanel> ();
			if (tabControl != null)
				tabControl.SetPanelActive (2);
			return;
		}

		m_CommitCount++;
		if (ValidateAnswer ()) {
			MyDialog.Instance.ShowMsgBox ("全部正确");
			StopCommit ();
		} else if (m_CommitCount > CommitMax) {
			MyDialog.Instance.ShowMsgBox ("超过三次填写错误，已给出正确答案");
			ShowAnswer ();
			StopCommit ();
		} else {
			MyDialog.Instance.ShowMsgBox ("填写错误，已标记错误项");
		}
	}

	void StopCommit () {
		m_CommitCount = CommitMax + 1;
		foreach (var item in m_InputFields) {
			item.readOnly = true; // disable inputs
		}
	}

	bool ValidateAnswer () {
		bool errorFound = false;
		foreach (var input in m_InputFields) {
			QuizHelp helper = input.GetComponent<QuizHelp> ();
			if (helper == null)
				continue;
			bool ok = helper.Validate ();
			if (!errorFound && !ok)
				errorFound = true;
		}
		return !errorFound;
	}

	void ShowAnswer () {
		foreach (var input in m_InputFields) {
			QuizHelp helper = input.GetComponent<QuizHelp> ();
			if (helper == null)
				continue;
			helper.ShowGoodAnswer ();
		}
	}
}
