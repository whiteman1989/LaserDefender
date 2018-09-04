using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
    public float timeToEnd = 6;

	// Use this for initialization
	void Start () {
        StartCoroutine(EndGamedelay());
	}

    IEnumerator EndGamedelay()
    {
        yield return new WaitForSeconds(6);
        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        manager.LoadLevel("Win");
    }
}
