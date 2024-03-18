using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AnimationManager;

public class MyListener : MonoBehaviour
{
    public GameObject CameraModifier;

    public float quadrantData;
    public float dialPercent;
    public float switchData;
    public float bttnData;
    public float proxData;

    float dialPercentPrevious = 0;
    float proxPrevious = 0;

    public Camera tropicCamera;
    public Camera oasisCamera;
    public Camera spaceCamera;

    float zoomMin = 0;
    float zoomMax = -100;

    public bool currCamera = true; // true = oasis, false = tropic
    float prevBttnData;

    bool buttonPressed = false;

    public GameObject[] animations;
    public GameObject[] backgrounds;
    public GameObject[] midgrounds;
    public GameObject[] foregrounds;


    bool cameraForward = false; // true = prev > curr, false = curr > prev
    ArduinoDataClass arduinoData = new ArduinoDataClass();

    // Use this for initialization
    void Start()
    {
        startCams();

    }

    //Update is called once per frame
    void Update()
    {
        //float cameraPos = 0;
        //cameraPos = Remap(proxData,217,0,zoomMin, zoomMax); // map the data
        //cameraPos = Clamp(cameraPos, zoomMin, zoomMax); // clamp the data
        //CameraModifier.transform.position = new Vector3(CameraModifier.transform.position.x, CameraModifier.transform.position.y, cameraPos); // move camera z to new pos
        zoomCameras();
        //toggleAnimation();
        switchScene();
        updateHues();
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


        // Zoom Camera
        //float cameraPos = 0;
        //if (dialPercent != dialPercentPrevious && (Mathf.Abs(dialPercentPrevious - dialPercent) > 2))
        //{
        //    //Debug.Log("upadated Old " + oldData);
        //    cameraForward = dialPercentPrevious > dialPercent;
        //    dialPercentPrevious = dialPercent;
        //    cameraPos = Mathf.Abs(50 - dialPercent);
        //    if (!cameraForward)
        //    {
        //        cameraPos = (cameraPos * -1.00f);
        //    }
        //}

        //// Toggle Animation
        //if (switchData == 0)
        //{
        //    animations = GameObject.FindGameObjectsWithTag("Animation");
        //    foreach (GameObject a in animations)
        //    {
        //        AnimationManager animationM = a.GetComponent<AnimationManager>();
        //        animationM.PauseAnimation();
        //    }
        //}
        //else
        //{
        //    animations = GameObject.FindGameObjectsWithTag("Animation");
        //    foreach (GameObject a in animations)
        //    {
        //        AnimationManager animationM = a.GetComponent<AnimationManager>();
        //        animationM.StartAnimation();
        //    }
        //}

        //// Switch Scene
        //if (bttnData != prevBttnData)
        //{
        //    prevBttnData = arduinoData.Bttn;

        //    if (bttnData == 1 && !buttonPressed)
        //    {
        //        buttonPressed = true;
        //        currCamera = !currCamera;
        //    }
        //    else if (bttnData == 0)
        //    {
        //        buttonPressed = false;
        //    }
        //}

        //Debug.Log("cameraForward " + cameraForward);
        //Debug.Log("cameraPos " + cameraPos);
        //CameraModifier.transform.position += new Vector3(0, 0, cameraPos * 0.5f);
        //camScene();



    }

    void updateHues()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        midgrounds = GameObject.FindGameObjectsWithTag("Midground");
        foregrounds = GameObject.FindGameObjectsWithTag("Foreground");
        float dialVal = Remap(dialPercent, 0, 100, 0, 1);
        Color newHue = Color.HSVToRGB(dialVal, 1, 1);

        if (quadrantData == 1)
        {
            foreach (GameObject b in backgrounds)
            {
                b.GetComponent<SpriteRenderer>().color = newHue;
            }
        }
        if (quadrantData == 2)
        {
            foreach (GameObject m in midgrounds)
            {
                m.GetComponent<SpriteRenderer>().color = newHue;
            }
        }
        if (quadrantData == 3)
        {
            foreach (GameObject f in foregrounds)
            {
                f.GetComponent<SpriteRenderer>().color = newHue;
            }
        }

    }
    void zoomCameras()
    {
        float cameraPos = 0;
        cameraPos = Remap(proxData, 217, 0, zoomMin, zoomMax); // map the data
        cameraPos = Clamp(cameraPos, zoomMin, zoomMax); // clamp the data

        // move each camera to the new z position
        oasisCamera.transform.position = new Vector3(oasisCamera.transform.position.x, CameraModifier.transform.position.y, cameraPos);
        tropicCamera.transform.position = new Vector3(tropicCamera.transform.position.x, CameraModifier.transform.position.y, cameraPos);
        spaceCamera.transform.position = new Vector3(spaceCamera.transform.position.x, CameraModifier.transform.position.y, cameraPos);
    }

    void toggleAnimation()
    {
        // Toggle Animation
        if (switchData == 0)
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

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;

    }

    public float Clamp(float val, float min, float max)
    {
        if (val < max)
        {
            return max;
        }
        else if (val > min)
        {
            return min;
        }
        else
        {
            return val;
        }
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

    public void startCams()
    {
        oasisCamera.enabled = true;
        tropicCamera.enabled = false;
        spaceCamera.enabled = false;

        //Debug.Log(tropicCamera.isActiveAndEnabled);

    }
    public void switchScene()
    {
        if (switchData == 0)
        {
            oasisCamera.enabled = true;
            tropicCamera.enabled = false;
            spaceCamera.enabled = false;
        }
        else if (switchData == 1)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = true;
            spaceCamera.enabled = false;
        }
        else if (switchData == 2)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = false;
            spaceCamera.enabled = true;
        }
    }
}