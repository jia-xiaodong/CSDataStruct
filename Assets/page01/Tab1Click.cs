using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab1Click : MonoBehaviour {
	MapTab m_Map;

	// Use this for initialization
	void Start () {
		Button tab = this.GetComponent<Button> ();
		tab.onClick.AddListener(OnClick);
		m_Map = FindObjectOfType<MapTab> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick() {
		m_Map.ShowBeijingMap ();
	}
}
