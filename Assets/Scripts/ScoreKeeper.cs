using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private static int score = 0;
	private Text scoreField;

	// Use this for initialization
	void Start () {
		scoreField = GetComponent<Text> ();
		Reset ();
	}
	

	public int Score 
	{
		get
		{
			return score;
		}
		set
		{
			score += value;
			UpdateSoreField();
		}
	}


	public void Reset (){
		ResetScore ();
		UpdateSoreField();
	}

	public void SetScore(int points){
		score = points;
	}

	void UpdateSoreField(){
		scoreField.text = score.ToString();
	}

	public static void ResetScore(){
		score = 0;
	}

	public static int GetScore(){
		return score;
	}


}
