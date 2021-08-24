using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyPathfinding : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerController player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
    }

    public void HandlePathfinding()
    {
        if (agent.enabled == false) return;

        if (ThereIsACubeBelowMe() == true) agent.destination = player.transform.position;
        else TurnOffNavMeshAgent();
    }

    bool ThereIsACubeBelowMe()
    {
        Vector3 overlapBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
        Collider[] collidedObjects = Physics.OverlapBox(transform.position + Vector3.down, overlapBoxSize);

        foreach (Collider collider in collidedObjects)
        {
            FloorCube floorCube = collider.GetComponent<FloorCube>();
            if (floorCube != null) return true;
        }

        return false;
    }

    public void TurnOffNavMeshAgent()
    {
        agent.enabled = false;
    }

    public float AgentSpeed
    {
        set
        {
            agent.speed = value;
        }
    }
}
