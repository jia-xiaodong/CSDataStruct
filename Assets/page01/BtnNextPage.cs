using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnNextPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button nextPage = GetComponent<Button> ();
		nextPage.onClick.AddListener (OnClicked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClicked () {
		SceneManager.LoadScene ("page02", LoadSceneMode.Single);
	}
}
