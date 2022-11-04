using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingSound : MonoBehaviour
{
    public GameObject slideActivate;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            slideActivate.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            slideActivate.SetActive(false);
        }
    }
}
