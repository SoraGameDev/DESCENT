using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<Transform> waypoints;

    [Header("Variables")]
    [SerializeField] float speed;
    int currentWaypoint;

    private void Start()
    {
        if (waypoints.Count <= 0) return;
        currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        platformMovement();
    }



    void platformMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position,
       (speed * Time.deltaTime));

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) <= 0)
        {
            currentWaypoint++;
        }

        if (currentWaypoint != waypoints.Count) return;
        waypoints.Reverse();
        currentWaypoint = 0;
    }


}
