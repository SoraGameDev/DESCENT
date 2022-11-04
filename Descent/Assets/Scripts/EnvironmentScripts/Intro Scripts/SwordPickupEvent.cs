using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwordPickupEvent : MonoBehaviour
{
    public GameObject pickupText;
    public WeaponSwitch wp;
    public GameObject pistol;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemySpawn;
    public GameObject playerHP;
    public Animator textAnim;


    // Start is called before the first frame update
    void Start()
    {
        pickupText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pickupText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                wp.pistolEquipped = true;
                pistol.SetActive(true);
                GameObject.Destroy(gameObject);
                pickupText.SetActive(false);
                enemySpawn.SetActive(true);
                playerHP.SetActive(true);
                textAnim.SetTrigger("PickUp");
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickupText.SetActive(false);
    }
}
