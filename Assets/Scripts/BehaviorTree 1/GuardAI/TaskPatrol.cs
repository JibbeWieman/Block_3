using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    private float _waitTime = 4f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private int[] _waitWaypoints = { 1, 2, 19, 32, 43, 52, 64 };

    private float _minSpeed = 4f;
    private float _maxSpeed = 7f;
    private const float rotationSpeed = 90f;
    private float _maxDeviationAngle = 25f;

    private bool _isMovingForward = true;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        /*if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else
        {*/
        /* Transform wp = _waypoints[_currentWaypointIndex];

         Quaternion targetRotation = Quaternion.LookRotation(wp.position - _transform.position);
         _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

         if (Vector3.Distance(_transform.position, wp.position) < 0.5f)
         {
             _transform.position = wp.position;
             _waitCounter = 0f;
             _waiting = true;

             _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
         }
         else
         {
             _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
             _transform.LookAt(wp.position);
         }
     }*/
        /////////
        /*Transform wp = _waypoints[_currentWaypointIndex];

        // Calculate direction to the next waypoint
        Vector3 direction = (wp.position - _transform.position).normalized;

        // Calculate angle between current forward direction and direction to the next waypoint
        float angle = Vector3.Angle(_transform.forward, direction);

        // Rotate if the angle is greater than a threshold (e.g., 5 degrees)
        if (angle > 5f)
        {
            // Rotate the AI towards the target direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(_transform.position, wp.position) < 0.7f)
        {
            _transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;

            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
        else
        {
            _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
        }*/
        //////////

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        Vector3 direction = (targetWaypoint.position - _transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards the current waypoint with some randomness
        //float speed = Random.Range(_minSpeed, _maxSpeed);
        //_transform.position = Vector3.MoveTowards(_transform.position, targetWaypoint.position, speed * Time.deltaTime);

        //Vector3 deviation = Random.insideUnitCircle * _maxDeviationAngle;
        //Quaternion deviationRotation = Quaternion.Euler(deviation.x, deviation.y, deviation.z);
        //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, _transform.rotation * deviationRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(_transform.position, targetWaypoint.position) < 0.9f)
        {
            /*_transform.position = targetWaypoint.position;
            _waitCounter = 0f;
            _waiting = true;*/

            // Move to the next waypoint
            // Check if this is the last waypoint
            if (_currentWaypointIndex == 0)
            {
                // Move to the next waypoint
                _currentWaypointIndex++;
            }

            else if (_currentWaypointIndex == _waypoints.Length - 1)
            {

                // Move to the previous waypoint
                _currentWaypointIndex--;
            }
            else
            {
                // Move to the next waypoint
                //_currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                //_currentWaypointIndex++;
                if (_isMovingForward)
                {
                    _currentWaypointIndex++;
                }
                else
                {
                    _currentWaypointIndex--;
                }
            }
            //_currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            if (_currentWaypointIndex == _waypoints.Length - 1 || _currentWaypointIndex == 0)
            {
                _isMovingForward = !_isMovingForward;
            }

            // Check if the current waypoint index is in the list of waypoints where the AI should wait
            if (ArrayContains(_waitWaypoints, _currentWaypointIndex))
            {
                // Start waiting
                _waiting = true;
                _waitCounter = 0f;
                state = NodeState.RUNNING;
                return state;
            }
        }

        // If not waiting, continue moving towards the current waypoint with some randomness
        if (!_waiting)
        {
            float moveSpeed = Random.Range(_minSpeed, _maxSpeed);
            _transform.position = Vector3.MoveTowards(_transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
            Vector3 deviationAngle = Random.insideUnitCircle * _maxDeviationAngle;
            Quaternion deviationRotation = Quaternion.Euler(deviationAngle.x, deviationAngle.y, deviationAngle.z);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _transform.rotation * deviationRotation, rotationSpeed * Time.deltaTime);
        }

        // Increment wait counter if waiting
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;

            // Check if wait time has elapsed
            if (_waitCounter >= _waitTime)
            {
                _waiting = false; // Stop waiting
                _waitCounter = 0f; // Reset wait counter
            }
        }
    

        state = NodeState.RUNNING;
        return state;
    }

    // Helper method to check if an array contains a specific value
    private bool ArrayContains(int[] array, int value)
    {
        foreach (int element in array)
        {
            if (element == value)
            {
                return true;
            }
        }
        return false;
    }

    /*private int FindClosestWaypointIndex()
    {
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            float distance = Vector3.Distance(_transform.position, _waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }*/
}
