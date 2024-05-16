using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateM : State
{
    public ChaseStateM chaseState;
    public bool canSeeThePlayer;

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
