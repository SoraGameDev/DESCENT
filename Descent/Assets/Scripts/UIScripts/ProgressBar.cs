using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public Slider slider;
  
    public float chargeTime;
    public WeaponSwitch weaponSwitch;

    
    IEnumerator Start()
    {
        slider.value = 0.0f;
        float value = 0.0f;

        while (value <= 100f)
        {
            if(weaponSwitch.curWep == 0)
            {
                chargeTime = 0.3f;
                yield return new WaitForSeconds(chargeTime);
                UpdateSlider(value);
                value += 25.0f;
            }
            if (weaponSwitch.curWep == 1)
            {
                chargeTime = 0.5f;
                yield return new WaitForSeconds(chargeTime);
                UpdateSlider(value);
                value += 25.0f;
            }
            if (weaponSwitch.curWep == 2)
            {
                chargeTime = 0.3f;
                yield return new WaitForSeconds(chargeTime);
                UpdateSlider(value);
                value += 25.0f;
            }


        }
    }

 
    public void UpdateSlider(float value)
    {
        slider.value = value;
       
    }

   public void ShootGun(float value)
    {
        value = 0f;
        slider.value = value;
    }

}
