using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("References")]

    public Transform player;

    [SerializeField] LayerMask whatIsPlayer;

    Animator anim;

    [Header("Values")]
    public float health, damage, sightRange, attackRange, timeBetweenAttacks, moveSpeed;

    bool alreadyAttacked, playerInAttackRange, playerInSightRange;

    [Header("FMOD")]

    [SerializeField] EventReference attackSound;
    [SerializeField] EventReference hitSound;
    [SerializeField] EventReference deathSound;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Check for player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }


    void ChasePlayer()
    {
        UnityEngine.Debug.Log("Chasing Player");
        transform.LookAt(player);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
       // transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed);
    }

    void AttackPlayer()
    {
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            PlayerBehaviour pb = player.GetComponent<PlayerBehaviour>();
            RuntimeManager.PlayOneShot(attackSound);
            pb.TakePlayerDamage(damage);
            alreadyAttacked = true;
            Invoke(nameof(AttackReset), timeBetweenAttacks);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        RuntimeManager.PlayOneShot(hitSound);
        anim.SetTrigger("TakeDamage");
        if(health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        RuntimeManager.PlayOneShot(deathSound);
        Destroy(gameObject);
    }

    void AttackReset()
    {
        alreadyAttacked = false;
    }
}
