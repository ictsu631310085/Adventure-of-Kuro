using UnityEngine;

public class StardropController : BaseEnemyData
{
    // Reset Player MP
    public override void TakeDamage(int damage)
    {
        PlayerData player = GameObject.FindObjectOfType<PlayerData>();
        if (player.currentMP < player.maxMP)
        {
            player.currentMP = player.maxMP;
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
