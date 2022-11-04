using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnablerScript : MonoBehaviour
{

    [Header("Object/Text set to enable on entering a trigger")]
    public GameObject objecttoEnable;


    //Simple Functionality
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objecttoEnable.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objecttoEnable.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objecttoEnable.SetActive(false);
        }
        
    }
}
