using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEnabler : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enabledSteps()
    {
        player.GetComponent<FMODStudioFirstPersonFootsteps>().enabled = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enabledSteps();
        }
    }
}
