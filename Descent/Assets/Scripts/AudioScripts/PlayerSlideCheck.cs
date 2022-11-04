using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class PlayerSlideCheck : MonoBehaviour
{
    [Header("Refs and Vars")]

    [SerializeField] float rayDistance;

    [Header("FMOD")]

    FMOD.Studio.EventInstance slide;

    
    // Start is called before the first frame update
    void Start()
    {
       // 
    }
    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance slide)
    {
        slide.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE pS);
        return pS;
    }
    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            if (hit.collider.gameObject.CompareTag("PipeSlide"))
            {
                if (PlaybackState(slide) != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    UnityEngine.Debug.Log("Sliding");
                    slide = RuntimeManager.CreateInstance("event:/Player/SFX/Sliding");
                    slide.start();
                }else
                if (PlaybackState(slide) == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    slide.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    slide.release();
                }
            }
            
             

        }
    }

}
