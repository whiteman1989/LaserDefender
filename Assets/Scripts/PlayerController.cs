using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1f;
	public float minX = -5f;
	public float maxX = 5f;
	public GameObject projectile;
	public float projectileSeed = 1f;
	public float fireRate = 1f;
	public float reloadRate = 1f;
	public int maxHealth = 300;
	public GameObject HitParticle;
	public int maxAmmo = 3;
	public AudioClip error;

	private int currentHealth;

	public AudioClip gunSound;

	private int curentAmo;


	// Use this for initialization
	void Start () {
		FindMinAndMax (ref minX,ref maxX);
		curentAmo = maxAmmo;
		InvokeRepeating("Reload", 0.0000001f, reloadRate) ;
		currentHealth = maxHealth;
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
			currentHealth -= bullet.GetDamage();
			HitEffects(col);
			if (currentHealth <=0 ){
				Die ();
			}
			Debug.Log("Hit by a projectile "+currentHealth);
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
		if (curentAmo > 0) {
			Vector3 startPosition = transform.position + new Vector3 (0f, 0.7f, 0f);
			GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
			beam.rigidbody2D.velocity = Vector2.up * fireSpeed;
			AudioSource.PlayClipAtPoint (gunSound, startPosition);
			curentAmo--;
		} else {
			AudioSource.PlayClipAtPoint (error, gameObject.transform.position);
		}
	}

	void FireRate(){
		Fire (projectileSeed);
	}

	void Die ()
	{
		LevelManager manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		manager.LoadLevel ("Win");
		CancelInvoke("Reload");
		Destroy (gameObject);
	}

	void HitEffects(Collider2D col){
		Instantiate(HitParticle, new Vector3(col.transform.position.x, col.transform.position.y, 0), Quaternion.identity);
	}

	public int GetCurentAmo(){
		return curentAmo;
	}

	void Reload(){
		if (!Input.GetKey (KeyCode.Space)) {
			if (curentAmo < maxAmmo) {
				curentAmo++;
			}
		}
	}

	public int GetHealth(){
		return currentHealth;
	}
}
