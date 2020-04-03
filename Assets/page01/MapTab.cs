using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MapType {
	MapChina = 0,
	MapBeijing = 1,
	MapCount = 2
}

public class TabChangedEventArgs : System.EventArgs {
	public readonly MapType currentTab;

	public TabChangedEventArgs (MapType current) {
		currentTab = current;
	}
}

public class MapTab : MonoBehaviour {
	GeoGraph m_Map0;  // China map, directional map
	GeoGraph m_Map1;  // Beijing map, non-directional map

	static MapTab s_instance;

	// when user switches Tab, broadcast this event.
	public event System.EventHandler<TabChangedEventArgs> TabChanged;

	// Use this for initialization
	void Start () {
		GeoGraph[] maps = this.GetComponentsInChildren<GeoGraph> (true);
		foreach (GeoGraph i in maps) {
			if (i.CompareTag ("china"))
				m_Map0 = i;
			else if (i.CompareTag ("beijing"))
				m_Map1 = i;
		}
		TabChanged += m_Map0.OnTabChanged;
		TabChanged += m_Map1.OnTabChanged;

		ShowMap (MapType.MapChina); // display China map on startup, it's directed graph.

		s_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowMap (MapType type) {
		OnTabChanged (new TabChangedEventArgs (type));
	}

	protected virtual void OnTabChanged (TabChangedEventArgs e) {
		var tmp = TabChanged; // thread-safety trick
		if (tmp != null) {    // if subscriber list isn't empty
			tmp (this, e);    // broadcast event to them
		}
	}

	public static MapTab Instance {
		get { return s_instance; }
	}
}
