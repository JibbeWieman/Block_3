using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCoroutines : MonoBehaviour
{
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 5f;
    private float _rotationSpeed = 5f;
    //private float _waitTime = 1f; 

    private IEnumerator Start()
    {
        
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned!");
            yield break;
        }

        
        Debug.Log("Number of waypoints: " + waypoints.Length);

        while (true)
        {
            yield return StartCoroutine(MoveToNextWaypoint());
            //yield return new WaitForSeconds(_waitTime);
        }
    }

    private IEnumerator MoveToNextWaypoint()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        Debug.Log("Current waypoint index: " + _currentWaypointIndex);
        Debug.Log("Current waypoint position: " + wp.position);

        while (Vector3.Distance(transform.position, wp.position) > 0.3f)
        {
            Vector3 direction = (wp.position - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            Debug.Log("Moving towards waypoint");
            yield return null;
        }

        // Ensure exact waypoint position
        transform.position = wp.position;

        
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        Debug.Log("Next waypoint index: " + _currentWaypointIndex);
    }

}
