using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    [SerializeField] float lifetime;

    [SerializeField] float shotDamage = 6f;
    public GameObject bloodFX;
    public GameObject envFX;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.TakeDamage(shotDamage);

            GameObject newBloodFX = Instantiate(bloodFX);
         //   newBloodFX.transform.localScale = new Vector3(100, 100, 100);
           // Destroy(Instantiate(newBloodFX), 0.1f);
        }

        FlyingEnemyAI flyingEnemy = collision.gameObject.GetComponent<FlyingEnemyAI>();

        if (flyingEnemy != null)
        {
            flyingEnemy.TakeDamage(shotDamage);

            Instantiate(bloodFX);
        }

        ZombieEnemyAI zombieEnemy = collision.gameObject.GetComponent<ZombieEnemyAI>();

        if (zombieEnemy != null)
        {
            zombieEnemy.TakeDamage(shotDamage);
            Instantiate(bloodFX);
        }


        if (collision.gameObject.CompareTag("Environment"))
        {
            GameObject newEnvFX = Instantiate(envFX);
           // newEnvFX.transform.localScale = new Vector3(100, 100, 100);
            //Destroy(Instantiate(newEnvFX, collision.gameObject.transform.position, Quaternion.LookRotation(collision.gameObject.transform.position)), 0.2f);
        }

        Invoke(nameof(SelfDestruct), 0.05f);
    }

    

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
