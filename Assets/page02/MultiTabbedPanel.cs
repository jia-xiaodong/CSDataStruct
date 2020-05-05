using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTabbedPanel : MonoBehaviour {
	ScrollRect[] panels = new ScrollRect[3];

	// Use this for initialization
	void Start () {
		Button[] buttons = this.GetComponentsInChildren<Button> (true);
		int index = 0;
		foreach (var btn in buttons) {
			if (!btn.tag.StartsWith ("my_tag_"))
				continue;
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

	public void SetPanelActive (int index) {
		for (int i = 0; i < 3; i++) {
			if (index == i) {
				panels [i].gameObject.SetActive (true);
			} else {
				panels [i].gameObject.SetActive (false);
			}
		}
	}
}
