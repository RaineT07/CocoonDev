using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    public GameObject CameraModifier;
    float oldData = 0;
    float newData;
    bool cameraForward = false;

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
        //Debug.Log("Arduino Data: " + msg);
        newData = float.Parse(msg);
        float cameraPos = 0;


        if (newData != oldData && (Mathf.Abs(oldData - newData) > 3))
        {
            //Debug.Log("upadated Old " + oldData);
            cameraForward = oldData > newData;
            //changeHeight = oldData - newData;
            oldData = newData;
            cameraPos = Mathf.Abs(50 - newData);
            if (!cameraForward)
            {
                cameraPos = (cameraPos * -1.00f);
            }


        }

        //Debug.Log("cameraForward " + cameraForward);
        Debug.Log("cameraPos " + cameraPos);
        CameraModifier.transform.position += new Vector3(0, 0, cameraPos * 0.5f);




    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}