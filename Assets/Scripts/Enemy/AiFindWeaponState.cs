using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    public AiStateId GetID()
    {
        return AiStateId.FindWeapon;
    }

    public void Enter(AiAgent agent)
    {
        WeaponPickUp pickup = FindClosestWeapon(agent);
        agent.navMeshAgent.destination = pickup.transform.position;
        agent.navMeshAgent.speed = 5;
    }

    public void Update(AiAgent agent)
    {
        if (agent.weapons.HasWeapon())
        {
            agent.weapons.ActivateWeapon();
        }
    }

    public void Exit(AiAgent agent)
    {
        // Logic for exiting the FindWeapon state can be implemented here.
    }

    private WeaponPickUp FindClosestWeapon(AiAgent agent)
    {
        WeaponPickUp[] weapons = Object.FindObjectsOfType<WeaponPickUp>();
        WeaponPickUp closestWeapon = null;
        float closestDistance = float.MaxValue;

        foreach (var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if (distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closestWeapon = weapon;
            }
        }

        return closestWeapon;
    }
}
