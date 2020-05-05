using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHelp : MonoBehaviour {
	public string m_Answer;
	public System.Func<string, string, bool> m_Validator;

	// Use this for initialization
	void Start () {
		InitValidator ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string UserAnswer {
		get { return GetComponent<InputField> ().text; }
		set { GetComponent<InputField> ().text = value; }
	}

	public string GoodAnswer {
		get { return m_Answer; }
	}

	protected void InitValidator () {
		if (Validator == null) {
			Validator = this.DefaultValidator;
		}
	}

	public System.Func<string, string, bool> Validator {
		get { return m_Validator; }
		set { m_Validator = value; }
	}

	protected bool DefaultValidator (string userAnswer, string goodAnswer) {
		return (string.Compare (userAnswer, goodAnswer, false) == 0);
	}

	/* Check if InputField has correct answer.
	 * @return true if no error is found.
	 */
	public bool Validate () {
		string answer1 = UserAnswer.Trim ();
		string answer2 = GoodAnswer.Trim ();
		bool correct = m_Validator (answer1, answer2);
		Image image = gameObject.GetComponent<Image> ();
		Color bg = new Color (image.color.r, image.color.g, image.color.b, correct ? 0.0F : 1.0F);
		image.color = bg;
		return correct;
	}

	public void ShowGoodAnswer () {
		UserAnswer = GoodAnswer;
		Image image = gameObject.GetComponent<Image> ();
		Color bg = new Color (image.color.r, image.color.g, image.color.b, 0.0F);
		image.color = bg;
		//
		QuizTwinHelp mirror = GetComponent<QuizTwinHelp> ();
		if (mirror != null) {
			mirror.DuplicateInput (GoodAnswer);
		}
	}
}
