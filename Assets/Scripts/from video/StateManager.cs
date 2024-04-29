using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    void Start()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwicthToTheNextState(nextState);
        }
    }

    private void SwicthToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
