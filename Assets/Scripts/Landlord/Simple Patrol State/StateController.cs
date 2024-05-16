using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    NavMeshAgent agent;

    public IState currentState;

    //public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        ChangeState(patrolState);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        currentState.OnEntry(this);
    }

    void Update()
    {
        currentState.OnUpdate(this);
    }
}
