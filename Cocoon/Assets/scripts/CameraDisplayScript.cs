using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDisplayScript : MonoBehaviour
{
    public Camera tropicCamera;
    public Camera oasisCamera;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Pressed 1
        {
            ShowTropic();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // Pressed 2
        {
            ShowOasis();
        }
    }

    public void ShowOasis()
    {
        tropicCamera.enabled = false;
        oasisCamera.enabled = true;
    }

    public void ShowTropic()
    {
        tropicCamera.enabled = true;
        oasisCamera.enabled = false;
    }
}
