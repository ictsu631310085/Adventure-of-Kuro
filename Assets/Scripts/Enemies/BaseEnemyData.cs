using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyData : MonoBehaviour, IDamageable, IKillable, IStunable, IKnockbackable
{
    public abstract void TakeDamage(int damage);
    public abstract void Kill();
    public abstract void getStun(float stunTime);
    public abstract void getKnockback(float knockback, EnumHolder.Facing direction);
}
