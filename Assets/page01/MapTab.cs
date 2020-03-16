using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTab : MonoBehaviour {
	Image m_Map0;  // China map, directional map
	Image m_Map1;  // Beijing map, non-directional map
	// Use this for initialization
	void Start () {
		Image[] maps = this.GetComponentsInChildren<Image> ();
		foreach (Image i in maps) {
			if (i.CompareTag ("china"))
				m_Map0 = i;
			else if (i.CompareTag ("beijing"))
				m_Map1 = i;
		}
		this.ShowChinaMap ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowBeijingMap() {
		m_Map0.gameObject.SetActive (false);
		m_Map1.gameObject.SetActive (true);
	}

	public void ShowChinaMap() {
		m_Map0.gameObject.SetActive (true);
		m_Map1.gameObject.SetActive (false);
	}
}
