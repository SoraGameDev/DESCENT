using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoom : MonoBehaviour
{
    [Header("Put each door script in this list")]
    public List<ArenaDoor> doors = new List<ArenaDoor>();

    [Header("Set equal to amount of enemies that will be in this room")]
    public int enemiesInRoom;

    [HideInInspector] public int enemiesKilled;

    
    void FixedUpdate()
    {
        if(enemiesKilled == enemiesInRoom)
        {
            Debug.Log("Room Cleared");
            foreach(ArenaDoor arenaDoor in doors)
            {
                arenaDoor.roomCleared = true;
                enemiesKilled++;
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }
}
