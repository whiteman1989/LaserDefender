using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public GameObject player;

	private PlayerController playerController;
	private RectTransform healthbar;
	private Vector2 size;
	private float width;
	private float higth;
	private float oneHealthSize;

	// Use this for initialization
	void Start () {
		healthbar = gameObject.GetComponent<RectTransform> ();
		playerController = player.GetComponent<PlayerController>();
		size = healthbar.localScale;
		width = size.x;
		higth = size.y;
		oneHealthSize = width / (float)playerController.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.localScale = new Vector2(oneHealthSize * (float)playerController.GetHealth (), higth);
		if (playerController.GetHealth () <= 100) {
			gameObject.GetComponent<Image> ().color = Color.red;
		} else {
			gameObject.GetComponent<Image> ().color = Color.white;
		}
	}

}
