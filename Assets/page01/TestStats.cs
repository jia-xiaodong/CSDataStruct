using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestStats : MonoBehaviour {
	int[] m_ErrorCount;
	bool[] m_TestDone;
	Text m_Text;
	System.Action m_ButtonAction;
	static TestStats s_instance;

	public const int ErrorMax = 3;

	// Use this for initialization
	void Start () {
		m_Text = GetComponentInChildren<Text> ();
		Button exit = GetComponentInChildren<Button> ();
		exit.onClick.AddListener (OnButtonClicked);

		s_instance = this;

		m_ErrorCount = new int[(int)MapType.MapCount];
		m_TestDone = new bool[(int)MapType.MapCount];

		ShowMessageBox (false); // hide itself after initialization that must be done when active.
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetErrorCount (MapType type) {
		return m_ErrorCount [(int)type];
	}

	public void IncErrorCount (MapType type) {
		m_ErrorCount [(int)type] += 1;
		if (m_ErrorCount [(int)type] > ErrorMax) {
			m_TestDone [(int)type] = true;
		}
	}

	public bool IsTestDone (MapType type) {
		return m_TestDone [(int)type];
	}

	public void SetTestDone (MapType type, bool isDone) {
		m_TestDone[(int)type] = isDone;
	}

	public string MessageText {
		set { m_Text.text = value; }
	}

	public void ShowMessageBox (bool show = true) {
		gameObject.SetActive (show);
	}

	public static TestStats Instance {
		get { return s_instance; }
	}

	public System.Action ButtonAction{
		set { m_ButtonAction = value; }
	}

	protected void OnButtonClicked () {
		if (m_ButtonAction != null) {
			m_ButtonAction.Invoke ();
		}
		m_ButtonAction = null;
		ShowMessageBox (false);
	}
}
