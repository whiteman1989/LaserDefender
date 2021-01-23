using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Shield : MonoBehaviour, IDamageble<int>, IAttached {

    [Header("Shield Specificaations")]
    [SerializeField] int shieldMaxEnergy = 400;
    [SerializeField] int shieldEnergyRegeneration = 10;
    [SerializeField] [Range(0f, 1f)] float beamDamageMult = 0.8f;
    [SerializeField]private int shieldCurrentEnergy;
    private Collider2D shieldCollider;
    private ParticleSystem[] shieldParticles;

    [Header("FX")]
    [Range(0f, 2f)]
    public float startEmissiveStr = 1.5f;
    [Range(0f, 2f)]
    public float lowLevel = 0.45f;

    private Renderer shieldRener;
    private PlayerController player;
    Animator animator;


    // Use this for initialization
    void Start()
    {
        shieldRener = GetComponent<Renderer>();
        player = gameObject.GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        shieldCurrentEnergy = shieldMaxEnergy;
        shieldCollider = GetComponent<Collider2D>();
        shieldParticles = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentEmisive = (float)shieldCurrentEnergy / (float)shieldMaxEnergy * startEmissiveStr;
        shieldRener.material.SetFloat("_EmmisionStrength", currentEmisive);
        animator.SetBool("LowLewel", currentEmisive <= lowLevel);

        IfDeath();

    }

    #region IDemagable
    public void Damage(int damageTaken, DamageTypes damageType)
    {
        float damageMult = 1;
        if (damageType == DamageTypes.Beam)
        {
            damageMult = beamDamageMult;
        }

        Damage((int)((float)damageTaken*damageMult));
    }

    public void Damage(int damageTaken)
    {
        Debug.Log("Shield get " + damageTaken + " damage");
        shieldCurrentEnergy -= damageTaken;
        if (shieldCurrentEnergy < 0)
        {
            Debug.Log("shield has " + shieldCurrentEnergy + "energy");
            shieldCurrentEnergy = 0;
        }
    }

    public void Healing(int healing)
    {
        shieldCurrentEnergy += healing;
    }

    public int GetMaxHealth()
    {
        return shieldMaxEnergy;
    }

    public int GetCurrentHealth()
    {
        return shieldCurrentEnergy;
    }

    public void SetHealth(int newHealth)
    {
        shieldCurrentEnergy = newHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        shieldMaxEnergy = newMaxHealth;
    }

    public void Kill()
    {
        SetHealth(0);
    }
    #endregion

    #region IAttached
    public Transform GetParentForAttached()
    {
        return transform.parent;
    }
    #endregion

    void IfDeath()
    {
        bool isNotDead = shieldCurrentEnergy > 0;
        //Debug.Log("isNotDead " + isNotDead);
        shieldCollider.enabled = isNotDead;
        animator.SetBool("Empty", !isNotDead);


    }


    #region Particles
    public void ParticlePlay()
    {
        foreach (ParticleSystem particle in shieldParticles)
        {
            particle.Play();
        }
    }

    public void ParticleStop()
    {
        foreach (ParticleSystem particle in shieldParticles)
        {
            particle.Stop();
        }
    }
    #endregion
}
