using UnityEngine;

public abstract class PlayerBaseState
{
    // Start
    public abstract void EnterState(PlayerStateManager stateManager, PlayerData playerData);

    // Update
    public abstract void UpdateState(PlayerStateManager stateManager, PlayerData playerData);
}
