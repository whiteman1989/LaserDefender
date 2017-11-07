using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public int health = 100;
	public float projectileSped = 8f;
	public float shotsPerSecond =1f; 

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
			if (health <=0 ){
				Destroy(gameObject);
			}
			Debug.Log("Hit by a projectile");
		}
	}

	void Fire(){
		Vector3 startPosition = transform.position + new Vector3 (0f, -0.6f, 0f);
		GameObject bullet = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		bullet.rigidbody2D.velocity = new Vector3 (0f, - projectileSped, 0f);
	}
}
