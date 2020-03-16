using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Btn2Click : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick() {
		SceneManager.LoadScene ("page01", LoadSceneMode.Single);
		Debug.Log ("Button 2 onClick event");
	}
}
