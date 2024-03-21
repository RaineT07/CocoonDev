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
        if(Input.GetKeyDown(KeyCode.Space)) {

            //Load scene
            SceneManager.LoadScene(2);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }

    }
}
