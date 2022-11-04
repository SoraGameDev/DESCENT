using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScriptCollision : MonoBehaviour
{

    public GameObject player;
    public GameObject resetPos;
    public PistolGrapple pg;
    public bool isReset = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.transform.position = resetPos.transform.position;
            player.transform.rotation = resetPos.transform.rotation;
            isReset = true;
            Invoke(nameof(GrappleReset), 0.5f);
            pg.enabled = false;

        }
    }

    public void GrappleReset()
    {
        isReset = false;
        pg.enabled = true;
    }
}
