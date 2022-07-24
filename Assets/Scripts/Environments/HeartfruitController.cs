using UnityEngine;

public class HeartfruitController : BaseEnemyData
{
    // Reset Player HP
    public override void TakeDamage(int damage)
    {
        PlayerData player = GameObject.FindObjectOfType<PlayerData>();
        if (player.currentHP < player.maxHP)
        {
            player.currentHP = player.maxHP;
            Kill();
        }
    }

    // Destroy When Used
    public override void Kill()
    {
        Destroy(gameObject);
    }

    // Unused
    public override void getStun(float stunTime)
    {
        
    }

    // Unused
    public override void getKnockback(float knockback, EnumHolder.Facing direction)
    {
        
    }
}
