using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Haul : MonoBehaviour {
	public GameObject player;

	private PlayerController playerController;
	private Text haulText;

	// Use this for initialization
	void Start () {
		haulText = this.GetComponent<Text>();
		playerController = player.GetComponent<PlayerController>();
	
	}
	
	// Update is called once per frame
	void Update () {
		haulText.text = playerController.GetHealth().ToString();
		if (playerController.GetHealth() <= 100) {
			haulText.color = Color.red;
		} else {
			haulText.color = Color.white;
		}
	
	}
}
