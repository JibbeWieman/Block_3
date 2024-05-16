using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEntry(StateController controller);

    void OnUpdate(StateController controller);

    void OnExit(StateController controller);
}
