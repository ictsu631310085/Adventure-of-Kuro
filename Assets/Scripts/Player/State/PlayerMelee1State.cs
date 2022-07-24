using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee1State : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Melee1;

        playerData.MeleeDash(200);
        playerData.MeleeHitCheck();
        playerData.animator.SetTrigger("Melee_1");
        playerData.InstMeleeFX(stateManager);
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        playerData.rb2D.velocity = new Vector2(0, playerData.rb2D.velocity.y);
        playerData.animator.SetBool("isWalking", false);

        if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Melee Attack 1") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Melee Attack 1") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Melee Attack 1"))
        {
            stateManager.SwitchState(stateManager.MeleeT1State);
        }
    }
}
