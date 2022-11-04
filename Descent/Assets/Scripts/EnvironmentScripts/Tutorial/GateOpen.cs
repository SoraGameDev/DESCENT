using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    


    [Header("References")]
    [SerializeField] List<Transform> waypoints;
    public Rigidbody player;
    public GameObject soundEnabler;
    [Header("Variables")]
    [SerializeField] float speed;
    float playerSpeed;
    int currentWaypoint;

    private void Start()
    {
       
        currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position,
       (speed * Time.deltaTime));

        playerSpeed = player.GetComponent<Rigidbody>().velocity.magnitude;

        if (playerSpeed >= 40)
        {
            currentWaypoint = 1;
            Debug.Log("Move Gates");
            soundEnabler.SetActive(true);
        }
        if (playerSpeed <= 40)
        {
            currentWaypoint = 0;
            soundEnabler.SetActive(false);
        }
    }

}

