using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartSpeed : MonoBehaviour
{
    public Rigidbody playerRB;

    [SerializeField] float dropSpeed;
    
    void Start()
    {
        playerRB = playerRB.GetComponent<Rigidbody>();
        // At the start of a level, drop the player so the fall down the pipe seems realistic (prevents floating)
        playerRB.AddForce(0, -dropSpeed, 0);
    }

}
