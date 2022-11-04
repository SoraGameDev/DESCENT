using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ArenaDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<Transform> doorPositions;
    BoxCollider roomTrigger;
    [SerializeField] EventReference doorClose;
    [SerializeField] EventReference doorOpen;

    [Header("Variables")]
    public int currentPos;
    public float speed;
    public bool roomCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        roomTrigger = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, doorPositions[currentPos].transform.position,
       (speed * Time.deltaTime));

        if (roomCleared == true)
        {
            UnlockArenaDoor();
            roomCleared = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomTrigger.enabled = false;

            LockArenaDoor();
        }

       
    }

    public void LockArenaDoor()
    {
        currentPos = 1;
        RuntimeManager.PlayOneShot(doorClose);
    }

    public void UnlockArenaDoor()
    {
        currentPos = 0;
        RuntimeManager.PlayOneShot(doorOpen);
    }
}
