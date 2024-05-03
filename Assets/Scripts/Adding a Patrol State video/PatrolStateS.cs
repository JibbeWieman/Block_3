using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
public class PatrolStateS : AbstractFSMState
{
    NPCPatrolPoint[] _patrolPoints;
    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            _patrolPoints = _npc.PatrolPoints;

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                Debug.LogError("PatrolState: Failed to grab patrol points from the NPC");
            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }

                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
            }
        }

        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            /*if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }*/

            // Check if the NPC has reached the current patrol point
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                // Move to the next patrol point
                _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                SetDestination(_patrolPoints[_patrolPointIndex]);

                // Debug logs to track NPC movement
                Debug.Log("NPC reached patrol point. Moving to next point.");
                Debug.Log("NPC Position: " + _navMeshAgent.transform.position);
                Debug.Log("Patrol Point Position: " + _patrolPoints[_patrolPointIndex].transform.position);
                Debug.Log("Setting Destination: " + _patrolPoints[_patrolPointIndex].transform.position);
            }
        }
    }

    private void SetDestination(NPCPatrolPoint destination)
    {
        if(_navMeshAgent != null & destination != null)
        {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
