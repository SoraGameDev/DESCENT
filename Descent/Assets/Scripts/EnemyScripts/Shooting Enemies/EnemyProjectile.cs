using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public LayerMask whatIsPlayer;
    public GameObject explosion;

    [Header("Bullet Variables")]
    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;
    public float explosionRange;
    public float explosionDamage;

    //Lifetime
    int collisions;
    PhysicMaterial physics_mat;


    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode
        if (collisions > maxCollisions)
        {
            Explode();
        }
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Explode();
        }
    }

    private void Setup()
    {
        //Create material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        
        //Assign material
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<PlayerBehaviour>().TakePlayerDamage(explosionDamage);
        }

        Invoke(nameof(Delay), 0.5f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if (collision.collider.CompareTag("Player") && explodeOnTouch)
        {
            Explode();
        }
    }

}
