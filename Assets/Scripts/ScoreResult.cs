using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour {
	private Text scoreField;

	// Use this for initialization
	void Start () {
		scoreField = GetComponent<Text> ();
		scoreField.text = ScoreKeeper.GetScore().ToString();
	}

}
