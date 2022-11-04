using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class PistolGrapple : MonoBehaviour
{
    [Header("References")]
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleableObjects;
    public Transform grappleTip, cameraTransform, player;
    private SpringJoint joint;
    public GameObject knife, knifePos, grappleIcon;
    public Camera cam;
    public bool enemyGrappled = false;
    public ResetScriptCollision rs;

    [Header("Variables")]
    [SerializeField] public float maxDist;
    public bool canGrapple;
    public float grappleCooldown;

    [Header("FMOD")]
    [SerializeField] EventReference grappleSound;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    private void OnDisable()
    {
        StopGrapple();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && rs.isReset == false)
        {
            if(canGrapple == true)
            {
                StartGrapple();
            }
            
            
        }
        if (Input.GetButtonUp("Fire2") && rs.isReset == false)
        {
            StopGrapple();
        }
        


        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDist, grappleableObjects) && canGrapple == true)
        {
            grappleIcon.SetActive(true);
        }
        else grappleIcon.SetActive(false);

        if(rs.isReset == true)
        {
            lr.positionCount = 0;
            Destroy(joint);
            knife.transform.position = knifePos.transform.position;
            //knife.transform.rotation = knifePos.transform.rotation;
            cam.fieldOfView = 90f;
            Invoke(nameof(grappleReset), grappleCooldown);
        }

    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        //Sending out raycast to find the grapple point, then adding a joint to the player to allow the physics of grappling
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDist, grappleableObjects) && canGrapple == true)
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distancefromPoint = Vector3.Distance(player.position, grapplePoint);

            //Adjust Values
            joint.maxDistance = distancefromPoint * 0.8f;
            joint.minDistance = distancefromPoint * 0.25f;

            //Adjust Values
            joint.spring = 23f;
            joint.damper = 7f;
            joint.massScale = 4f;

            //Line renderer
            lr.positionCount = 2;

            //Grapple sound
            FMODUnity.RuntimeManager.PlayOneShot(grappleSound, grappleTip.position);
            //Need effect to make grappling feel more like youre grappling
            cam.fieldOfView = 90f;
            //Freeze enemy in place when grappled
            if (hit.transform.gameObject.tag == "Enemy")
            {
                enemyGrappled = true;
            }
            else enemyGrappled = false;
            canGrapple = false;

        }
    }
    
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        knife.transform.position = knifePos.transform.position;
        knife.transform.rotation = knifePos.transform.rotation;
        cam.fieldOfView = 90f;
        Invoke("grappleReset", grappleCooldown);
    }

    void DrawRope()
    {
        if (!joint) return;
        
        lr.SetPosition(0, grappleTip.position);
        lr.SetPosition(1, grapplePoint);
        knife.transform.position = lr.GetPosition(1);
    }

    public void grappleReset()
    {
        canGrapple = true;
    }
}
