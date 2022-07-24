using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee3State : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Melee3;

        playerData.getKnockback(500, playerData.facing);
        playerData.Invoke("MeleeHitCheck",0.2f);
        playerData.animator.SetTrigger("Melee_3");
        playerData.InstMeleeFX(stateManager);
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        playerData.rb2D.velocity = new Vector2(0, playerData.rb2D.velocity.y);

        if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Melee Attack 3") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Melee Attack 3") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Melee Attack 3"))
        {
            stateManager.SwitchState(stateManager.IdleState);
        }
    }
}
