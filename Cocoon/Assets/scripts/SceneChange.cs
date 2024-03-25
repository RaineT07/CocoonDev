using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Bedroom") {
            SceneManager.LoadScene(2);//loading screen
        }

        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Main Scene")
        {
            SceneManager.LoadScene(0); //bedroom, might want to create an "endgame" bedroom scene.
      
        }

    }
}
