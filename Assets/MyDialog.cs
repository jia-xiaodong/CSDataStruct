using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyDialog : MonoBehaviour {
	static MyDialog s_instance;

	Text m_Text;
	System.Action m_ButtonAction;

	// Use this for initialization
	void Start () {
		s_instance = this;
		m_Text = GetComponentInChildren<Text> ();
		Button exit = GetComponentInChildren<Button> ();
		exit.onClick.AddListener (this.OnButtonClicked);
		ShowMsgBox ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static MyDialog Instance {
		get { return s_instance; }
	}

	public void ShowMsgBox (string msg = null) {
		if (msg == null) {
			gameObject.SetActive (false);
		} else {
			m_Text.text = msg;
			gameObject.SetActive (true);
		}
	}

	public System.Action ButtonAction {
		set { m_ButtonAction = value; }
	}

	protected void OnButtonClicked () {
		if (m_ButtonAction != null) {
			m_ButtonAction.Invoke ();
		}
		m_ButtonAction = null;
		ShowMsgBox ();  // close message box
	}
}
