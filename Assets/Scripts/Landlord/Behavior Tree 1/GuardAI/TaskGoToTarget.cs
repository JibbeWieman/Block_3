using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private Transform _target;

    // Threshold distance to stop following the player
    private float _distanceThreshold = 20f;
    private bool _isFollowing = false;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        _target = (Transform)GetData("target");

        // Debug log to check if the target is being received correctly
        //Debug.Log("Target received: " + _target);

        /*if (target == null)
        {
            Debug.LogWarning("No target found for AI.");
            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector3.Distance(_transform.position, target.position);
        Debug.Log("Distance to target: " + distance);

        if (distance > 0.5f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, GuardBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
            state = NodeState.RUNNING;
        }
        else
        {
            state = NodeState.SUCCESS;
        }

        return state;*/

        /*if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, GuardBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }

        state = NodeState.RUNNING;
        return state;*/




        if (_target != null)
        {
            /*float distanceThreshold = 10f;

            float distanceToTarget = Vector3.Distance(_transform.position, _target.position);

            if (distanceToTarget > distanceThreshold)
            {
                // Player is out of range, stop following
                parent.ClearData("target");
                return NodeState.FAILURE;
            }

            // Move towards the target
            float speed = GuardBT.speed;
            float step = speed * Time.deltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

            // Rotate towards the target
            Vector3 direction = (_target.position - _transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, step * 100);

            Debug.Log("Node state: " + state);*/

            float distanceToTarget = Vector3.Distance(_transform.position, _target.position);

            if (distanceToTarget <= _distanceThreshold)
            {

                // Move towards the received target (player)
                float speed = GuardBT.speed;
                float step = speed * Time.deltaTime;
                _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

                // Rotate towards the target
                Vector3 direction = (_target.position - _transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, step * 100);

                _isFollowing = true;
                return NodeState.RUNNING;
            }
            else
            {
                // Player moved too far away, stop following
                _isFollowing = false;
                return NodeState.FAILURE;
            }
        }

        // If not following, return failure
        _isFollowing = false;
        return NodeState.FAILURE;
    }

    // Method to check if the AI is currently following the player
    public bool IsFollowingPlayer()
    {
        return _isFollowing;
    }
}
