using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class Enemy : MonoBehaviour
{
    [Header("Variables and References")]
    public LayerMask ignoreLayer;
    public float health = 200f;
    public float damage = 10f;
    public float range = 2f;
    public bool canAttack;
    public GameObject wep;
    public FirstLevelEnemyEvent enemyCount;
    public GameObject player;
    public Transform playerTransform;
    public PistolGrapple pg;
    Rigidbody rb;

    [Header("EnemyStats")]
    int MoveSpeed = 4;
    //int MaxDist = 10;
    int MinDist = 5;

    

    [Header("FMOD")]

    [SerializeField] EventReference takeDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, playerTransform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        UnityEngine.Debug.DrawRay(wep.transform.position, wep.transform.forward, Color.red, range);
        Attack();

        if (pg.enemyGrappled == true)
        {
            rb.isKinematic = true;
        }
        else rb.isKinematic = false;
    }

        public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Death();
        }

        FMODUnity.RuntimeManager.PlayOneShot(takeDamage, transform.position);
    }

    public void Death()
    {
        enemyCount.enemiesKilled++;
        Destroy(gameObject);
    }

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(wep.transform.position, wep.transform.forward, out hit, range, ~ignoreLayer))
        {
            UnityEngine.Debug.Log(hit.transform.name);
            

            PlayerBehaviour player = hit.transform.GetComponent<PlayerBehaviour>();
            if (player != null && canAttack == true && hit.transform.tag == ("Player"))
            {
                canAttack = false;
           
                player.TakePlayerDamage(damage);

                Invoke("attackReset", 1f);
            }
        }
    }

    public void attackReset()
    {
        canAttack = true;

    }
}
