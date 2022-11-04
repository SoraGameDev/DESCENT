using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class ZombieEnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    [HideInInspector] Transform player;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    Animator anim;

    [Header("Enemy Values")]

    public float health = 100;



    [Header("Attack Values")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] float damage;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("FMOD")]

    [SerializeField] EventReference takeDamage;
    [SerializeField] EventReference dealDamage;
    [SerializeField] EventReference deathSound;

    private void Awake()
    {
        //Get Components
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
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
        if(!playerInAttackRange && !playerInSightRange)
        {
            anim.SetBool("Running", false);
        }
    }

    private void ChasePlayer()
    {
        //Move to player
        agent.SetDestination(player.position);
        

        //Animation
        anim.SetBool("Running", true);
    }

    private void AttackPlayer()
    {
        //Stand in place
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack Code
            PlayerBehaviour pb = player.GetComponent<PlayerBehaviour>();
            pb.TakePlayerDamage(damage);
            Invoke(nameof(DamageSound), 0.5f);
            alreadyAttacked = true;
            Invoke(nameof(AttackReset), timeBetweenAttacks);

            //Animation
            anim.SetTrigger("AttackPlayer");
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Invoke(nameof(Death), 0.2f);
        }

        RuntimeManager.PlayOneShot(takeDamage);
    }
    private void AttackReset()
    {
        alreadyAttacked = false;
    }

    public void Death()
    {
        RuntimeManager.PlayOneShot(deathSound, transform.position);
        Destroy(gameObject);
    }

    private void DamageSound()
    {
        RuntimeManager.PlayOneShot(dealDamage, transform.position);
    }
}
