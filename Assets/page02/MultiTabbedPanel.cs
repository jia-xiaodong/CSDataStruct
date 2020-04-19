using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTabbedPanel : MonoBehaviour {
	Button[] buttons = new Button[3];
	ScrollRect[] panels = new ScrollRect[3];

	// Use this for initialization
	void Start () {
		buttons = this.GetComponentsInChildren<Button> ();
		for (int i = 0; i < buttons.Length; i++) {
			panels[i] = buttons [i].GetComponentInChildren<ScrollRect> (true);
			int j = i;  // lambda trick: capture value immediately
			buttons [i].onClick.AddListener (() => this.SetPanelActive (j));
		}
		SetPanelActive (0);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void SetPanelActive (int index) {
		for (int i = 0; i < 3; i++) {
			if (index == i) {
				panels [i].gameObject.SetActive (true);
			} else {
				panels [i].gameObject.SetActive (false);
			}
		}
	}
}
