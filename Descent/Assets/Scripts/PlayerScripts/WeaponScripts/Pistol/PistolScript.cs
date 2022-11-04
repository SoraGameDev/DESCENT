using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using UnityEngine.UI;
using TMPro;

public class PistolScript : MonoBehaviour
{
    //Variables
    [Header("Variables")]
    public float damage = 10f;
    public float range = 100f; 
    public float fireRate;
   
    public int maxAmmo;
    public int currentAmmo;
    public int reloadTime;
   

    public bool canShoot = true;
    
    
    //Scene Object References
    [Header("References")]
    public Camera cam;
    public GameObject gunBarrel;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    public TextMeshPro ammoCount;
    public GameObject bloodFX;
    public GameObject rockFX;
    public LayerMask shootableLayers;
    public GameObject bullet;
    public GameObject bulletPoint;
    //Script References
    public UIManager uiManager;
    public ProgressBar progressBar;

    [Header("FMOD")]
    [SerializeField] EventReference GunshotPath;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
       
        
        animator.SetBool("Reloading", false);
       
    }

    // Update is called once per frame
    void Update()
    {
   
        //Inputs
        if (Input.GetButtonDown("Fire1") && canShoot == true && uiManager.gamePaused == false)
        {
            Shoot();
            ShootBullet();
            animator.SetTrigger("Shoot");
            RateofFire();
            //progressBar.slider.value -= 100f;
          //  progressBar.UpdateSlider(-100f);
        }

        
    }

    IEnumerator Reload()
    {
        
        UnityEngine.Debug.Log("Reload");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        


    }

    public void Shoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GunshotPath, gunBarrel.transform.position);

        muzzleFlash.Play();



        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range, shootableLayers))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                GameObject newBloodFX = Instantiate(bloodFX);
                newBloodFX.transform.localScale = new Vector3(100, 100, 100);
                Destroy(Instantiate(newBloodFX, hit.point, Quaternion.LookRotation(hit.normal)), 1f);

            }

            FlyingEnemyAI flyingEnemy = hit.transform.GetComponent<FlyingEnemyAI>();
            if(flyingEnemy != null)
            {
                flyingEnemy.TakeDamage(damage);

                GameObject newBloodFX = Instantiate(bloodFX);
                newBloodFX.transform.localScale = new Vector3(100, 100, 100);
                Destroy(Instantiate(newBloodFX, hit.point, Quaternion.LookRotation(hit.normal)), 1f);
            }

            ZombieEnemyAI zombieEnemy = hit.transform.GetComponent<ZombieEnemyAI>();
            if(zombieEnemy != null)
            {
                zombieEnemy.TakeDamage(damage);
                GameObject newBloodFX = Instantiate(bloodFX);
                newBloodFX.transform.localScale = new Vector3(100, 100, 100);
                Destroy(Instantiate(newBloodFX, hit.point, Quaternion.LookRotation(hit.normal)), 1f);
            }

            if (hit.transform.gameObject.CompareTag("Environment"))
            {
                GameObject newRockFX = Instantiate(rockFX);
                newRockFX.transform.localScale = new Vector3(100, 100, 100);
                Destroy(Instantiate(newRockFX, hit.point, Quaternion.LookRotation(hit.normal)), 1f);
            }

        }
    }

    void ShootActivate()
    {
        
        canShoot = true;

    }

    void CleanUpChildren()
    {
        Destroy(bloodFX);
    }
  

    void RateofFire()
    {
        canShoot = false;
        Invoke(nameof(ShootActivate), fireRate);

    }

    void ShootBullet()
    {
        Rigidbody rb = Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
       
        rb.AddForce(transform.right * 600f, ForceMode.Impulse);
    }
}
