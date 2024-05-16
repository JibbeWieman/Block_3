using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Text;

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]

public class IdleStateS : AbstractFSMState
{
    [SerializeField] float _idleDuration = 3f;

    float _totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();
        if (EnteredState)
        {
            Debug.Log("ENTERED IDLE STATE");
            _totalDuration = 0f;
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            _totalDuration += Time.deltaTime;
            Debug.Log("UPDATING IDLE STATE:" + _totalDuration + " seconds.");
            if (_totalDuration >= _idleDuration)
            {
                _fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting IDLE STATE");
        return true;
    }
}
