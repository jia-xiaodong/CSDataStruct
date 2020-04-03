using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab1Click : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Button tab = this.GetComponent<Button> ();
		tab.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick() {
		MapTab.Instance.ShowMap (MapType.MapBeijing);
	}
}
