using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [Header("Projectile Variables")]
    //projectile variables
    public float velocity;
    public float liveTime;
    [Header("Damage Stats")]
    //damage stats
    public float damage;
    public float explosionForce;
    public float blastRadius;
    public float enemyDamage;
    [Header("Explosion FX")]
    //public references
    public GameObject explosionFX;

    Vector3 explosionPos;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity;

        Invoke(nameof(Explode), liveTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    
    private void Update()
    {
        explosionPos = transform.position;
    }
    public void Explode()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, blastRadius);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].tag == "Player")
            {
                PlayerBehaviour playerBehaviour = players[i].gameObject.GetComponent<PlayerBehaviour>();
                StrafeMovement strafeMovement = players[i].gameObject.GetComponent<StrafeMovement>();
                Rigidbody playerRB = players[i].GetComponent<Rigidbody>();
                if (playerBehaviour != null)
                {
                    //strafeMovement.AddImpact(explosionForce, transform.position - players[i].transform.position);
                    playerRB.AddExplosionForce(explosionForce, explosionPos, blastRadius);
                    playerBehaviour.TakePlayerDamage(damage);
                }
            }
            if(players[i].tag == "Enemy")
            {
                EnemyAI enemyAI = players[i].gameObject.GetComponent<EnemyAI>();

                if (enemyAI != null)
                {
                    enemyAI.TakeDamage(enemyDamage);
                }

                FlyingEnemyAI flyingEnemy = players[i].gameObject.GetComponent<FlyingEnemyAI>();

                if(flyingEnemy != null)
                {
                    flyingEnemy.TakeDamage(enemyDamage);
                }
            }
        }
        Instantiate(explosionFX, transform.position, transform.rotation);
        //Destroy(thisExplosionFX, 2f);  <- asset already has self destruct code
        Destroy(gameObject);
    }

}
