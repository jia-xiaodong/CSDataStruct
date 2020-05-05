using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnNextPage : MonoBehaviour {

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToNextPage () {
		Scene current = SceneManager.GetActiveScene ();
		int index;
		if (!int.TryParse (current.name.Substring (4), out index))
			return;
		string next = string.Format ("page{0:D2}", index + 1);
		SceneManager.LoadScene (next, LoadSceneMode.Single);
	}

	public void ToPrevPage () {
		Scene current = SceneManager.GetActiveScene ();
		int index;
		if (!int.TryParse (current.name.Substring (4), out index))
			return;
		string prev = string.Format ("page{0:D2}", index - 1);
		SceneManager.LoadScene (prev, LoadSceneMode.Single);
	}

	public void AppExit () {
		Application.Quit ();
	}
}
