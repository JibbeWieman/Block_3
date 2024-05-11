/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _playerLayerMask = 1 << 6;

    private Transform _transform;
    private Transform _playerTransform;
    //private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();

        _playerLayerMask = LayerMask.GetMask("Player");
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            float fovRange = GuardBT.fovRange;
            float distanceThreshold = 10f;

            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GuardBT.fovRange, _playerLayerMask);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    float distanceToPlayer = Vector3.Distance(_transform.position, collider.transform.position);
                    Debug.Log("Distance to player: " + distanceToPlayer);

                    if (distanceToPlayer <= distanceThreshold)
                    {
                        _playerTransform = collider.transform;
                        parent.parent.SetData("target", _playerTransform);
                        return NodeState.SUCCESS;
                    }
                }
            }

            /*float fovRange = GuardBT.fovRange; // Adjust FOV range as needed
            float distanceThreshold = 10f; // Adjust distance threshold as needed

            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, fovRange, _playerLayerMask);

            if (colliders.Length > 0)
            {
                // Check if the player is within the distance threshold
                foreach (Collider collider in colliders)
                {
                    if (Vector3.Distance(_transform.position, collider.transform.position) <= distanceThreshold)
                    {
                        parent.parent.SetData("target", collider.transform);
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }*/

            /*state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

}
*/