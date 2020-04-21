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
		int index = 0;
		foreach (var btn in buttons) {
			ScrollRect scrollView = btn.GetComponentInChildren<ScrollRect> (true);
			if (scrollView == null) {
				continue;
			}
			panels [index] = scrollView;
			int immediate_index = index++;  // lambda trick: capture value immediately
			btn.onClick.AddListener (() => this.SetPanelActive (immediate_index));
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
