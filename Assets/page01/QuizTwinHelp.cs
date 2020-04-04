using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTwinHelp : QuizHelp {
	public Text m_Mirror;

	// Use this for initialization
	void Start () {
		base.InitValidator ();

		if (m_Mirror != null) {
			InputField i = GetComponent<InputField> ();
			i.onEndEdit.AddListener (DuplicateInput);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DuplicateInput (string input) {
		m_Mirror.text = input;
	}
}
