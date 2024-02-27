using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    public GameObject CameraModifier;
    float oldData;
    float newData;

    // Use this for initialization
    void Start()
    {

    }
    //Update is called once per frame
    void Update()
    {
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Arduino Data: " + msg);
        newData = float.Parse(msg);
        //oldData = newData;
        float changeHeight = 50 - newData;

        CameraModifier.transform.position += new Vector3(0,0,Time.deltaTime * changeHeight);




    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}