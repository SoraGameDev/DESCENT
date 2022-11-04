using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public GameObject checkpoint;
    public Animator checkpointIcon;
    BoxCollider trigger;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            checkpoint.transform.SetPositionAndRotation(transform.position, transform.rotation);

            checkpointIcon.SetTrigger("Checkpoint");
            Destroy(gameObject);
        }
    }
}
