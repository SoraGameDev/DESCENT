using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<Transform> waypoints;
    public GameObject soundEnabler;
    [Header("Variables")]
    [SerializeField] float speed;
    int currentWaypoint;
    private void Start()
    {
        currentWaypoint = 0;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position,
       (speed * Time.deltaTime));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentWaypoint = 1;
            Debug.Log("Move Gates");
            soundEnabler.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentWaypoint = 0;
            soundEnabler.SetActive(false);
        }
    }
}
