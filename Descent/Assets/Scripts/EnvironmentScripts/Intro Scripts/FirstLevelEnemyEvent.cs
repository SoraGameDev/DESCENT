using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelEnemyEvent : MonoBehaviour
{
    public int enemiesKilled = 0;

    public GameObject floor;
    private void Update()
    {
        if(enemiesKilled == 2)
        {
            floor.SetActive(false);
        }
    }
}
