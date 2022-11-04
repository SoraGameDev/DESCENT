using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class BossStart : MonoBehaviour
{
    private Animator anim;
    public Animator sunrayAnim;
    public GameObject demoendMenu;
    public MouseLook ml;
    public GameObject playerWeapons;
    public Rigidbody rb;
    public UIManager uiManager;
    BoxCollider trigger;
    [SerializeField] EventReference roomShake;
    public GameObject sunSound;
    public GameObject menuMusic;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BossAnimation();
        }
    }

    void BossAnimation()
    {
        trigger.enabled = false;
        RuntimeManager.PlayOneShot(roomShake);
        anim.SetTrigger("BossStarted");

        Invoke(nameof(SunAnimation), 5f);

        Invoke(nameof(EndDemo), 9.8f);
    }

    void SunAnimation()
    {
        sunrayAnim.SetTrigger("BossStarted");
        sunSound.SetActive(true);

        
    }

    void EndDemo()
    {

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
