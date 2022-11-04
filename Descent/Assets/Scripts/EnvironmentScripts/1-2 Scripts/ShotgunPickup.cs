using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickup : MonoBehaviour
{
    public float speed;
    public WeaponSwitch wS;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject text;
    public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            enemies.SetActive(true);
            text.SetActive(true);
            wS.shotgunEquipped = true;
            wS.curWep++;

            SelfDestruct();
        }
    }


    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
