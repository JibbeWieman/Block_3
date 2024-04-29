using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEntry(StateController controller)
    {
        // This will be called when first entering the state
    }

    public void OnUpdate(StateController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.ChangeState(controller.patrolState);
        }
    }

    public void OnExit(StateController controller)
    {
        // This will be called on leaving the state
    }
}
