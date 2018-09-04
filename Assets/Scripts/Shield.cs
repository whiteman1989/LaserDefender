using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [Range(0f, 2f)]
    public float startEmissiveStr = 1.5f;
    [Range(0f, 2f)]
    public float lowLevel = 0.3f;

    private Renderer shieldRener;
    private PlayerController player;
    Animator animator;

    // Use this for initialization
    void Start () {
        shieldRener = GetComponent<Renderer>();
        player = gameObject.GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float currentEmisive = (float)player.GetHealth() / (float)player.maxHealth * startEmissiveStr;
        shieldRener.material.SetFloat("_EmmisionStrength", currentEmisive);
        animator.SetBool("LowLewel", currentEmisive<=lowLevel);

    }
}
