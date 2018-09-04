using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
	public float speed = 1f;
    [SerializeField] float padding = 1f;

    [Header("Gun specifications")]
	public GameObject projectile;
	public float projectileSpeed = 1f;
	public float fireRate = 1f;
	public float reloadRate = 1f;
    public int maxAmmo = 3;

    [Header("Hull specifications")]
    public int maxHealth = 300;

    [Header("Audio")]
    public AudioClip error;
    public AudioClip gunSound;
    public AudioClip hitSound;
    public AudioClip destroySound;

    [Header("FX")]
    public GameObject HitParticle;
    public GameObject destroyEffect;
    public GameObject derbish;


    private int currentHealth;
    private int curentAmo;
    private GameObject mainCamera;
    private float minX = -5f;
    private float maxX = 5f;
    private float minY = -5f;
    private float maxY = 5f;


    // Use this for initialization
    void Start () {
		FindMinAndMax (ref minX,ref maxX, ref minY, ref maxY);
		curentAmo = maxAmmo;
		InvokeRepeating("Reload", 0.0000001f, reloadRate) ;
		currentHealth = maxHealth;
        mainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InvokeRepeating("FireRate", 0.000001f, fireRate);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("FireRate");
        }

        Move();
    }

    private void Move()
    {
        //horiontal move
        float deltaPosX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaPosY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float newPosX = Mathf.Clamp(transform.position.x + deltaPosX, minX, maxX );
        float newPosY = Mathf.Clamp(transform.position.y + deltaPosY, minY, maxY);
        transform.position = new Vector3(newPosX, newPosY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col){
		Projectile bullet = col.gameObject.GetComponent<Projectile> ();
		if (bullet) {
			currentHealth -= bullet.GetDamage();
			HitEffects(col);
			if (currentHealth <=0 ){
				Die ();
			}
			//Debug.Log("Hit by a projectile "+currentHealth);
		}
	}

	void FindMinAndMax (ref float minX, ref float maxX, ref float minY, ref float maxY){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));
		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;
        minY = leftmost.y + padding;
        maxY = rightmost.y - padding;
	}

	void Fire (float fireSpeed){
		if (curentAmo > 0) {
			Vector3 startPosition = transform.position + new Vector3 (0f, 0.7f, 0f);
			GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = Vector2.up * fireSpeed;
			AudioSource.PlayClipAtPoint (gunSound, startPosition);
			curentAmo--;
		} else {
			AudioSource.PlayClipAtPoint (error, gameObject.transform.position);
		}
	}

	void FireRate(){
		Fire (projectileSpeed);
	}

	void Die ()
	{
		CancelInvoke("Reload");
        Instantiate(destroyEffect, gameObject.transform.position, Quaternion.identity);
        Instantiate(derbish, gameObject.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(destroySound, gameObject.transform.position);
        Destroy (gameObject);
    }

	void HitEffects(Collider2D col){
        //Debug.Log(name + " Hiting");
        mainCamera.GetComponent<CameraShake>().ShakeCamera(0.8f, 1f);
		Instantiate(HitParticle, new Vector3(col.transform.position.x, col.transform.position.y, 0), Quaternion.identity);
        AudioSource.PlayClipAtPoint(hitSound, col.transform.position);

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
