using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public int health = 100;
	public float projectileSped = 8f;
	public float shotsPerSecond =1f; 
	public int scoreValue = 150;
	public AudioClip gunSound;
	public GameObject hitEffect;
	public GameObject destroyEffect;
    public GameObject derbish;
	public AudioClip destriySound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update(){
		float prodaditly = Time.deltaTime * shotsPerSecond;
		if (Random.value < prodaditly) {
			Fire();
		}

	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile bullet = col.gameObject.GetComponent<Projectile> ();
		if (bullet) {
			health -= bullet.GetDamage();
			HitEffects(col);
			if (health <=0 ){
				scoreKeeper.Score = scoreValue;
				DestroyEffect(col);
				Destroy(gameObject);
			}
			//Debug.Log("Hit by a projectile");
		}
	}

	void Fire(){
		Vector3 startPosition = transform.position + new Vector3 (0f, -0.6f, 0f);
		GameObject bullet = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, - projectileSped, 0f);
		AudioSource.PlayClipAtPoint (gunSound, startPosition);
	}

	void HitEffects(Collider2D col){
		Instantiate(hitEffect, new Vector3(col.transform.position.x, col.transform.position.y, 0), Quaternion.identity);
	}

	void DestroyEffect (Collider2D col){
		Instantiate(destroyEffect, new Vector3(col.transform.position.x, col.transform.position.y, 0), Quaternion.identity);
        Instantiate(derbish, new Vector3(col.transform.position.x, col.transform.position.y, 0), Quaternion.identity);
        AudioSource.PlayClipAtPoint (destriySound, col.transform.position);
	}
}
