using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Shotgun : MonoBehaviour
{
    [Header("Variables and References")]
    public GameObject shotgunBullet;
    public GameObject player;
    public Transform gunBarrel;
    List<Quaternion> pellets;
    List<Quaternion> altPellets;
    Animator anim;

    public float maxSpread;
    public float bulletVelocity;
    public float velocity;
    public float fireRate;
    public float altFireRate;
    public float playerSpeed;
    public float playerKBSpeed;
    public float altFireCD;

    public Rigidbody playerRB;
    
    public int bulletsShot;
    public int altBulletsShot;

    public bool canFire;
    public bool canAltFire;

    Vector3 jumpPos;

    [Header("FMOD")]
    [SerializeField] EventReference shotSound;
    [SerializeField] EventReference altShotSound;

    private void Awake()
    {
        pellets = new List<Quaternion>(bulletsShot);
        for (int i = 0; i < bulletsShot; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }

        altPellets = new List<Quaternion>(altBulletsShot);
        for (int i = 0; i < altBulletsShot; i++)
        {
            altPellets.Add(Quaternion.Euler(Vector3.zero));
        }

        canFire = true;
        canAltFire = true;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = playerRB.GetComponent<Rigidbody>().velocity.magnitude;

        bulletVelocity = (velocity + playerSpeed * 2);

        jumpPos = gunBarrel.TransformDirection(Vector3.forward * -playerKBSpeed);

        if(playerSpeed >= 100)
        {
            bulletVelocity = (velocity + playerSpeed * 4);
        }


        if(Input.GetButtonDown("Fire1") && canFire)
        {
            Shoot();
            RuntimeManager.PlayOneShot(shotSound);
            canFire = false;
            Invoke(nameof(RateOfFire), fireRate);
            anim.SetTrigger("Shoot");
        }
        if (Input.GetButtonDown("Fire2") && canAltFire)
        {
            AltShoot();
            RuntimeManager.PlayOneShot(altShotSound);
            canAltFire = false;
            Invoke(nameof(AltFireCD), fireRate * altFireCD);
            anim.SetTrigger("Shoot");
        }

    }

    void Shoot()
    {

        for(int i = 0; i < bulletsShot; i++)
        {
            //Calculate Spread
            pellets[i] = Random.rotation;


            //Instantiate Bullet
            GameObject bullet = Instantiate(shotgunBullet, gunBarrel.position, gunBarrel.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, pellets[i], maxSpread);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletVelocity);
          


        }

    }

    void AltShoot()
    {
        for(int i = 0; i < altBulletsShot; i++)
        {
            //Calculate Spread
            altPellets[i] = Random.rotation;

            //Instantiate Bullet
            GameObject bullet = Instantiate(shotgunBullet, gunBarrel.position, gunBarrel.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, altPellets[i], maxSpread);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletVelocity);

            //Player Knockback
            player.GetComponent<Rigidbody>().AddForce(jumpPos, ForceMode.Impulse);
        }
    }
    
    void RateOfFire()
    {
        canFire = true;
    }
    void AltFireCD()
    {
        canAltFire = true;
    }
}
