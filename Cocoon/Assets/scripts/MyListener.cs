using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AnimationManager;

public class MyListener : MonoBehaviour
{
    public GameObject CameraModifier;
    float dialPercentPrevious = 0;

    public float dialPercent;
    public float switchData;
    public float bttnData;
    public float proxData;

    public Camera tropicCamera;
    public Camera oasisCamera;

    public bool currCamera = true; // true = oasis, false = tropic
    float prevBttnData;

    public GameObject[] animations;


    bool cameraForward = false;
     ArduinoDataClass arduinoData = new ArduinoDataClass();

    // Use this for initialization
    void Start()
    {
        tropicCamera.enabled = false;

    }

    //Update is called once per frame
    void Update()
    {
    }

    //Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arduino Data: " + msg);
        //parseArduinoData("Dial:100,Switch:0");
        parseArduinoData(msg);

        switchData = arduinoData.Switch;
        dialPercent = arduinoData.Dial;
        proxData = arduinoData.Prox;

        bttnData = arduinoData.Bttn;
        if (bttnData != prevBttnData)
        {
            prevBttnData = arduinoData.Bttn;
        }
        //bttnData = arduinoData.Bttn;


        // Zoom Camera
        float cameraPos = 0;
        if (dialPercent != dialPercentPrevious && (Mathf.Abs(dialPercentPrevious - dialPercent) > 2))
        {
            //Debug.Log("upadated Old " + oldData);
            cameraForward = dialPercentPrevious > dialPercent;
            dialPercentPrevious = dialPercent;
            cameraPos = Mathf.Abs(50 - dialPercent);
            if (!cameraForward)
            {
                cameraPos = (cameraPos * -1.00f);
            }
        }

        // Toggle Animation
        if(switchData == 0)
        {
            animations = GameObject.FindGameObjectsWithTag("Animation");
            foreach (GameObject a in animations)
            {
                AnimationManager animationM = a.GetComponent<AnimationManager>();
                animationM.PauseAnimation();
            }
        }
        else
        {
            animations = GameObject.FindGameObjectsWithTag("Animation");
            foreach (GameObject a in animations)
            {
                AnimationManager animationM = a.GetComponent<AnimationManager>();
                animationM.StartAnimation();
            }
        }

        // Switch Scene
        if(bttnData == 1 && prevBttnData == 0)
        {
            tropicCamera.enabled = false;
            oasisCamera.enabled = true;
        } else if (bttnData == 0 && prevBttnData == 1)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = true;
        }
        //if (currCamera)
        //{
        //    tropicCamera.enabled = false;
        //    oasisCamera.enabled = true;
        //}
        //else
        //{
        //    oasisCamera.enabled = false;
        //    tropicCamera.enabled = true;
        //}


        //Debug.Log("cameraForward " + cameraForward);
        //Debug.Log("cameraPos " + cameraPos);
        CameraModifier.transform.position += new Vector3(0, 0, cameraPos * 0.5f);




    }
    //Data is a string in this format: "Dial:100,Switch:0"
    void parseArduinoData(string data)
    {
        string[] ardData = data.Split(",");

        arduinoData.Dial = float.Parse(ardData[0].Split(":")[1]);
        arduinoData.Switch = float.Parse(ardData[1].Split(":")[1]);
        arduinoData.Bttn = float.Parse(ardData[2].Split(":")[1]);
        arduinoData.Prox = float.Parse(ardData[3].Split(":")[1]);
        //Debug.Log("Dial: " + arduinoData.Dial);
        //Debug.Log("Switch: " + arduinoData.Switch);
    }

    public class ArduinoDataClass
    {
        public float Dial;
        public float Switch;
        public float Bttn;
        public float Prox;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}