using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button exit = GetComponent<Button> ();
		exit.onClick.AddListener (OnClicked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClicked () {
		Application.Quit ();
	}
}
