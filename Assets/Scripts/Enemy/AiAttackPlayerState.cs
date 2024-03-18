using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    public AiStateId GetID()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.ActivateWeapon();
        agent.weapons.SetTarget(agent.playerTransform);
        agent.navMeshAgent.stoppingDistance = 5.0f;
        agent.weapons.SetFiring(true);
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;

        if (agent.playerTransform.GetComponent<Health>().IsDead())
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }
}
