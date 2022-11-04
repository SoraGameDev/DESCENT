using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSc : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    [Header("References")]
    public Rigidbody rb;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnemies;
    public GameObject explosion;

    [Header("Bullet Variables")]
    //Stats
    [Range(0f, 1f)]
    public float bounciness;

    //Damage
    public float explosionRange;
    public float explosionDamage;
    public float explosionForce;
    //Lifetime
    int collisions;
    PhysicMaterial physics_mat;

    private void Awake()
    {
      //  Setup();
    }

    void Start()
    {
        
        Invoke(nameof(Kill), lifetime);
    }

  
   

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        
    }

    public void Kill()
    {
        Destroy(gameObject);
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

    }
    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        Vector3 explosionPos = transform.position;
        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < player.Length; i++)
        {
            Rigidbody playerRB = player[i].GetComponent<Rigidbody>();

            player[i].GetComponent<PlayerBehaviour>().TakePlayerDamage(explosionDamage);
            playerRB.AddExplosionForce(explosionForce, explosionPos, explosionRange);

        }
        Kill();
    }
}
