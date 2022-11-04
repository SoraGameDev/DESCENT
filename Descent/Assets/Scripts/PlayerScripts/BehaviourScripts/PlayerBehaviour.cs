using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public StrafeMovement sm;
    public MouseLook ml;
    public WeaponSway ws;
    public GameObject player;
    public GameObject playerDir;
    public float health;
    public float playerSpeed;
    public bool playerDead;
    public TextMeshPro playerHealth;
    public GameObject deathScreen;
    public UIManager uiManager;
    public float rocketForce;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = health.ToString();
        if (health <= 0f)
        {
            health = 0;
            Death();
        }

        playerSpeed = rb.GetComponent<Rigidbody>().velocity.magnitude;

        if (playerSpeed >= 40)
        {
            health++;
        }
        if (health > 100)
        {
            health = 100f;
        }

        direction = playerDir.transform.position * rocketForce;

    }

    public void TakePlayerDamage(float amount)
    {
        health -= amount;
      


    }

    public void RocketJump()
    {
        rb.GetComponent<Rigidbody>().AddForce(-direction, ForceMode.Impulse);
    }
    public void Death()
    {
        playerDead = true;
        rb.isKinematic = true;
        sm.enabled = false;
        ml.enabled = false;
        ws.enabled = false;
        deathScreen.SetActive(true);
        uiManager.gamePaused = true;
    }
}
