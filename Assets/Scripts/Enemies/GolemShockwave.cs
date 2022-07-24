using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemShockwave : MonoBehaviour
{
    public EnumHolder.Facing facing;

    public int attackDamage;
    public float hitStun;
    public float knockback;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x == 1)
        {
            facing = EnumHolder.Facing.Left;
        }
        else if (transform.localScale.x == -1)
        {
            facing = EnumHolder.Facing.Right;
        }
    }

    // Trigger
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();

            playerData.TakeDamage(attackDamage);
            playerData.getStun(hitStun);
            playerData.getKnockback(knockback, facing);

            Destroy(gameObject);
        }
    }*/

    // Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();

            playerData.TakeDamage(attackDamage);
            playerData.getStun(hitStun);
            playerData.getKnockback(knockback, facing);

            Destroy(gameObject);
        }
    }
}
