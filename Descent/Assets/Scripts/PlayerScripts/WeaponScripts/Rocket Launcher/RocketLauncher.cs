using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class RocketLauncher : MonoBehaviour
{
    public bool canFire;
    public float fireRate = 0.5f;
    public Transform rocketBarrel;
    public GameObject projectile;
    public ProgressBar progressBar;
    [SerializeField] EventReference rocketShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

        if (Input.GetButtonDown("Fire1") && canFire == true)
        {
            RuntimeManager.PlayOneShot(rocketShot, rocketBarrel.transform.position);
            Instantiate(projectile, rocketBarrel.position, rocketBarrel.rotation);
            // progressBar.slider.value -= 100f;
            canFire = false;
            Invoke(nameof(ResetFire), fireRate);
        }
    }

    void ResetFire()
    {
        canFire = true;
    }
}
