using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    public FirstRoom firstRoom;

    private void OnDestroy()
    {
        firstRoom.EnemyKilled();
    }
}
