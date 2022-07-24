using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeT2State : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.MeleeT2;
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        if (playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Melee Transition 2") ||
            playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Melee Transition 2") ||
            playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Melee Transition 2"))
        {
            if (Time.timeScale == 1 && Input.GetButtonDown("Attack"))
            {
                stateManager.SwitchState(stateManager.Melee3State);
            }
        }
        else
        if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Melee Transition 1") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Melee Transition 1") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Melee Transition 1"))
        {
            stateManager.SwitchState(stateManager.IdleState);
        }
    }
}