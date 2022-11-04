using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 8f;
    
    public bool canSwing = true;
    
    public Camera cam;
    public Animator animator;
    public GameObject player;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canSwing == true)
        {
            Swing();
        }



    }

    public void Swing()
    {
        canSwing = false;
        animator.SetTrigger("Swing");

        Invoke("swingReset", 1f);

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                canSwing = false;
                animator.SetTrigger("Swing");
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                enemy.TakeDamage(damage);
                Invoke("swingReset", 1f);
            }

            

        }
    }

    public void swingReset()
    {
        canSwing = true;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0) && canSwing == true)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    

                    
                    enemy.TakeDamage(damage);
                    
                }
                

            }
            else
            {if (canSwing == true)
                Swing();
            }
        }
        
    }
}
