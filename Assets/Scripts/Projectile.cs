using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, IProjectile<int> {
	[SerializeField] int damage = 100;
    [SerializeField] DamageTypes damageType = DamageTypes.Beam;
    [SerializeField] float projectileSpeed = 30;

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageble<int> damagable = col.gameObject.GetComponent<IDamageble<int>>();
        if (damagable != null)
        {
            damagable.Damage(damage, damageType);
        }
        Destroy(gameObject);
    }


    public int GetDamage(){
		return damage;
	}

    public DamageTypes GetDamageType()
    {
        return damageType;
    }

    public float GetStartSpeed()
    {
        throw new System.NotImplementedException();
    }

    void Hit(){
		Destroy (gameObject);
	}


}
