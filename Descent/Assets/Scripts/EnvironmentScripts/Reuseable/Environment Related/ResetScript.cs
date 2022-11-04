using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = resetPos.transform.position;
            player.transform.rotation = resetPos.transform.rotation;
            isReset = true;
            Invoke(nameof(GrappleReset), 0.5f);

        }
        
    }

    public void GrappleReset()
    {
        isReset = false;

    }
}
