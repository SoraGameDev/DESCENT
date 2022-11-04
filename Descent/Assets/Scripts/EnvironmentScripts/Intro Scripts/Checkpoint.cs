using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject player;
    public GameObject checkpointPos;
    public GameObject resetpointPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            disableSteps();
            resetpointPos.transform.position = checkpointPos.transform.position;
            resetpointPos.transform.rotation = checkpointPos.transform.rotation;
            Debug.Log("Checkpoint");
            
        }
    }

    void disableSteps()
    {
        player.GetComponent<FMODStudioFirstPersonFootsteps>().enabled = false;
        
    }

    
}
