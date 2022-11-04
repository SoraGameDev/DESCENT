using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Invoke("SelfDestruct", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelfDestruct()
    {

        Destroy(gameObject);
    }
}
