using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && navMeshAgent.enabled)
        {
            // make enemy follow player
            navMeshAgent.SetDestination(player.position);
        }
    }
}
