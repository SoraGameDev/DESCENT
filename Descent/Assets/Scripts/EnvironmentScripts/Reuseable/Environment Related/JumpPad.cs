using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class JumpPad : MonoBehaviour
{
    [Header("References")]
    public Transform jumpRef;
    public GameObject player;
    [SerializeField] float jumpForce;
    Vector3 direction;

    [Header("FMOD")]
    [SerializeField] EventReference jumpSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        direction = jumpRef.TransformDirection(Vector3.forward * jumpForce);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            Debug.Log("JumpPad");
            RuntimeManager.PlayOneShot(jumpSound);
        }
    }
}
