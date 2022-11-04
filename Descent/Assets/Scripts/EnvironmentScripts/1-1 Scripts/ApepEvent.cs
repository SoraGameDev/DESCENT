using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApepEvent : MonoBehaviour
{
    public GameObject playerCamera;
    Animator anim;
    Vector3 originalCamPos;
    public GameObject floors;
    public Animator flooranim;
    public GameObject jumppads;
    private void Awake()
    {
        

        anim = GetComponent<Animator>();

        anim.SetTrigger("Event");

     //   StartCoroutine(CamShake(15, 20));

        Invoke(nameof(SelfDestruct), 15);
        Invoke(nameof(ActivateFloors), 1);
    }

    public IEnumerator CamShake(float duration, float magnitude)
    {
        originalCamPos = playerCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude; 
            float y = Random.Range(-1f, 1f) * magnitude;

            playerCamera.transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        playerCamera.transform.position = originalCamPos;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void ActivateFloors()
    {
        floors.SetActive(true);
        //flooranim.SetTrigger("Apep");
        jumppads.SetActive(true);
    }
}
