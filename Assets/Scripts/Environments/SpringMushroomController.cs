using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMushroomController : MonoBehaviour
{
    private Animator animator;

    public int force;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStateManager playerState = collision.gameObject.GetComponent<PlayerStateManager>();
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();

            if (playerState.currentState == PlayerStateManager.State.Falling ||
                playerState.currentState == PlayerStateManager.State.JumpAttacking)
            {
                playerData.rb2D.AddForce(Vector2.up * force);
                animator.SetTrigger("Bounce");
            }
        }
    }
}
