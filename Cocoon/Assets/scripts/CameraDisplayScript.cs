using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public Camera tropicCamera;
    public Camera oasisCamera;


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
