using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 300;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
