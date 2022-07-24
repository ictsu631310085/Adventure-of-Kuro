using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState playerState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerWalkingState WalkingState = new PlayerWalkingState();
    public PlayerJumpingState JumpingState = new PlayerJumpingState();
    public PlayerFallingState FallingState = new PlayerFallingState();
    public PlayerMelee1State Melee1State = new PlayerMelee1State();
    public PlayerMeleeT1State MeleeT1State = new PlayerMeleeT1State();
    public PlayerMelee2State Melee2State = new PlayerMelee2State();
    public PlayerMeleeT2State MeleeT2State = new PlayerMeleeT2State();
    public PlayerMelee3State Melee3State = new PlayerMelee3State();
    public PlayerJumpAttackingState JumpAttackingState = new PlayerJumpAttackingState();
    public PlayerDoubleJumpingState DoubleJumpingState = new PlayerDoubleJumpingState();
    public PlayerDashingState DashingState = new PlayerDashingState();

    private PlayerData playerData;

    public enum State
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Melee1,
        MeleeT1,
        Melee2,
        MeleeT2,
        Melee3,
        JumpAttacking,
        DoubleJumping,
        Dashing,
    }
    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GetComponent<PlayerData>();

        playerState = IdleState;
        playerState.EnterState(this, playerData);
    }

    // Update is called once per frame
    void Update()
    {
        playerState.UpdateState(this, playerData);
    }

    public void SwitchState(PlayerBaseState state)
    {
        playerState = state;
        state.EnterState(this, playerData);
    }
}
