using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee2State : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Melee2;

        playerData.MeleeDash(300);
        playerData.MeleeHitCheck();
        playerData.animator.SetTrigger("Melee_2");
        playerData.InstMeleeFX(stateManager);
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        playerData.rb2D.velocity = new Vector2(0, playerData.rb2D.velocity.y);

        if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Melee Attack 2") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Melee Attack 2") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Melee Attack 2"))
        {
            stateManager.SwitchState(stateManager.MeleeT2State);
        }
    }
}
