using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1f;
	public float minX = -5f;
	public float maxX = 5f;
	public GameObject projectile;
	public float projectileSeed = 1f;
	public float fireRate = 1f;
	public int health = 300;

	// Use this for initialization
	void Start () {
		FindMinAndMax (ref minX,ref maxX);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("FireRate", 0.000001f, fireRate) ;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("FireRate");
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}	

		float newX = Mathf.Clamp (transform.position.x, minX, maxX);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile bullet = col.gameObject.GetComponent<Projectile> ();
		if (bullet) {
			health -= bullet.GetDamage();
			if (health <=0 ){
				Destroy(gameObject);
			}
			Debug.Log("Hit by a projectile "+health);
		}
	}

	void FindMinAndMax (ref float min, ref float max){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));
		min = leftmost.x;
		max = rightmost.x;
	}

	void Fire (float fireSpeed){
		Vector3 startPosition = transform.position + new Vector3 (0f, 0.7f, 0f);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = Vector2.up * fireSpeed; 
	}

	void FireRate(){
		Fire (projectileSeed);
	}

}
