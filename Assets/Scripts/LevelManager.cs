using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name){
		//Debug.Log ("Load level requested for: " + name);
		SceneManager.LoadScene (name);
	}

	public void QuitRequest (){
		//Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel () {
        Scene curentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (curentScene.buildIndex + 1);
	}
	
}
