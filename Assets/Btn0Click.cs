using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn0Click : MonoBehaviour {
	private const string TEXT = "本实验通过交互式案例的方式，让学习者进一步掌握图的基本概念，存储方式、遍历等实际应用。培养学生用图理论解决实际问题的能力，使学习者结合真实问题，深入理解重要理论的使用方法，并能直观理解方法的具体执行过程和核心概念。";
	public Text m_text;
	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener(OnClick);
		this.OnClick();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick() {
		m_text.text = TEXT;
		Debug.Log ("Button 0 onClick event");
	}
}
