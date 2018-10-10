using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageTypes {Kinetic, Electric, Missile, Beam, none };

public interface IDamageble<T>
{
    void Damage(T damageTaken);
    void Damage(T damageTaken, DamageTypes damageType);
    void Healing(T healing);
    T GetMaxHealth();
    T GetCurrentHealth();
    void SetHealth(T newHealth);
    void SetMaxHealth(T newMaxHealth);
    void Kill();
}


public interface IShutable
{
    void Fire();
}

public interface IReloadable<T>
{
    void Reload();
    void Reload(T ammo);
    T GetCurrentAmmo();
    T GetMaxAmmo();
}

public interface IActivable
{
    void SetActivation(bool isActve);
    bool ActivationStatus();

}

public interface IPylon
{
    Transform GetPylonPosition();
    int GetPylonId();
}

public interface IProjectile<T>
{
    T GetDamage();
    float GetStartSpeed();
    DamageTypes GetDamageType();
}

public interface IAttached
{
    Transform GetParentForAttached();
}