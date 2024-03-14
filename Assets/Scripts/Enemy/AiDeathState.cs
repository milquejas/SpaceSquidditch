using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;

    public AiStateId GetID()
    {
        return AiStateId.Death;
    }

    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        agent.ui.gameObject.SetActive(false);
        agent.mesh.updateWhenOffscreen = true;
        agent.weapons.DropWeapon();
    }

    public void Update(AiAgent agent)
    {
        // Logic to execute each frame during the Death state can be added here.
    }

    public void Exit(AiAgent agent)
    {
        // Logic to execute when exiting the Death state can be added here.
    }
}
