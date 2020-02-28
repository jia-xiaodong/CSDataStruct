using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn1Click : MonoBehaviour {
	private const string TEXT = "本实验采用多个自定义的图，结合具体的实际应用，通过交互式的方式按要求完成各测试点内容，以达到对图理论的学习和认知。在实验过程中启发学习者按照图的理论完成知识点学习，并结合生动的推演步骤，培养学生根据具体情况分析问题、掌握知识点的能力。\n1）根据有向图/无向图特征，交互式的学习图的基本知识\n2）根据有向图/无向图特征，交互式的建立图的邻接关系及关键程序\n3）根据图搜索理论，交互式的遍历图结构";
	public Text m_text;
	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick() {
		m_text.text = TEXT;
		Debug.Log ("Button 1 onClick event");
	}
}
