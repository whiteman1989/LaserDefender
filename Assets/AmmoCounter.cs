using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {
	public GameObject player;
	public GameObject ammoIcon;
	public Sprite fullAmmo;
	public Sprite emptyAmmo;
	public Sprite noAmmo;

	private int maxAmmo;
	private int currentAmmo;
	private PlayerController playerScript;

	
	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<PlayerController> ();
		maxAmmo = playerScript.maxAmmo;
		for (int i = 0; i < maxAmmo; i++) {
			GameObject icon = Instantiate (ammoIcon) as GameObject;
			icon.transform.SetParent(gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		int count = 0;
		foreach (Transform child in transform) {
			if (playerScript.GetCurentAmo() <= 0){
				child.gameObject.GetComponent<Image>().sprite = noAmmo;
			}else if (count >= playerScript.GetCurentAmo()){
				child.gameObject.GetComponent<Image>().sprite = fullAmmo;
			}else{
				child.gameObject.GetComponent<Image>().sprite = emptyAmmo;
			}
			count++;
		}
	}
}
