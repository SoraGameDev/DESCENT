using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToIntro : MonoBehaviour
{
    public UIManager ui;

    private void OnTriggerEnter(Collider other)
    {
        ui.GoToNextLevel();
    }
}
