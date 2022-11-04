using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int curWep = 0;

    
    public bool pistolEquipped = false;
    public bool shotgunEquipped = false;
    public bool railgunEquipped = false;

    void Update()
    {

        {
            int previousCurWep = curWep;

            if (Input.GetAxis("Mouse ScrollWheel") < 0f && pistolEquipped && shotgunEquipped == true)
            {
                if (curWep >= transform.childCount - 1)
                    curWep = 0;
                else
                    curWep++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && pistolEquipped && shotgunEquipped == true)
            {
                if (curWep <= 0)
                    curWep = transform.childCount - 1;
                else
                    curWep--;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && pistolEquipped == true)
            {
                curWep = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && shotgunEquipped)
            {
                curWep = 1;
            }

            if (previousCurWep != curWep)
            {
                SelectWep();
            }
        }
    }

    void SelectWep()
    {
        int i = 0;

        foreach(Transform wep in transform)
        {
            if (i == curWep)

                wep.gameObject.SetActive(true);
            else
                wep.gameObject.SetActive(false);
            i++;
        }
    }
}
