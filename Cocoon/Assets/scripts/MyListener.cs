using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AnimationManager;

public class MyListener : MonoBehaviour
{
    //public GameObject CameraModifier;

    // Arduino data vars
    public float quadrantData;
    public float dialPercent;
    public float switchData;
    public float proxData;
    public float bttnData;

    // Camera references
    public Camera tropicCamera;
    public Camera oasisCamera;
    public Camera spaceCamera;
    public Camera greyCamera;

    // Zoom variables
    float zoomMin = 0;
    float zoomMax = -100;
    float proxDataMax = 217;

    // Arrays of scene assets
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
        //toggleAnimation();
        // these all go in OnMessageArrived() but for testing they need to be in update bc I don't have an arduino
        zoomCameras();
        switchScene();
        updateHues();
    }

    //Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arduino Data: " + msg);
        parseArduinoData(msg);

        // Store arduino data
        switchData = arduinoData.Switch;
        dialPercent = arduinoData.Dial;
        proxData = arduinoData.Prox;
        bttnData = arduinoData.Bttn;

    }

    // Table Functions:

    // Selects a group of assets based on quadrant data
    // Changes the hue of those assets based on dial data
    void updateHues()
    {
        // get all assets
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        midgrounds = GameObject.FindGameObjectsWithTag("Midground");
        foregrounds = GameObject.FindGameObjectsWithTag("Foreground");

        // map the dial val to make a hue
        float dialVal = Remap(dialPercent, 0, 100, 0, 1);
        Color newHue = Color.HSVToRGB(dialVal, 1, 1);

        // loop through and set the hue
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

    // Zoom all cameras based on proximity data
    // 0 = zoomed in all the way, 217 = zoomed out all the way
    void zoomCameras()
    {
        float cameraPos = 0;
        cameraPos = Remap(proxData, proxDataMax, 0, zoomMin, zoomMax); // map the data
        cameraPos = Clamp(cameraPos, zoomMin, zoomMax); // clamp the data

        // move each camera to the new z position
        oasisCamera.transform.position = new Vector3(oasisCamera.transform.position.x, oasisCamera.transform.position.y, cameraPos);
        tropicCamera.transform.position = new Vector3(tropicCamera.transform.position.x, tropicCamera.transform.position.y, cameraPos);
        spaceCamera.transform.position = new Vector3(spaceCamera.transform.position.x, spaceCamera.transform.position.y, cameraPos);
    }

    // Toggle animation of all assets with the tag animation based on boolean
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







    // Utilities:

    // Parses the arduino data and stores it in an object
    // Data is a string in this format: "Dial:100,Switch:0"
    void parseArduinoData(string data)
    {
        string[] ardData = data.Split(",");

        arduinoData.Dial = float.Parse(ardData[0].Split(":")[1]);
        arduinoData.Switch = float.Parse(ardData[1].Split(":")[1]);
        arduinoData.Bttn = float.Parse(ardData[2].Split(":")[1]);
        arduinoData.Prox = float.Parse(ardData[3].Split(":")[1]);
        //Debug.Log("Dial: " + arduinoData.Dial);
    }

    // Maps data from one range to another
    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
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

    // Clamps values to a min and max
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

    // Class to store arduino data
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

    // set camera default state for testing
    public void startCams()
    {
        oasisCamera.enabled = true;
        tropicCamera.enabled = false;
        spaceCamera.enabled = false;
    }

    // control scene with "switch" data for testing
    public void switchScene()
    {
        if (switchData == 0)
        {
            oasisCamera.enabled = true;
            tropicCamera.enabled = false;
            spaceCamera.enabled = false;
            greyCamera.enabled = false;
        }
        else if (switchData == 1)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = true;
            spaceCamera.enabled = false;
            greyCamera.enabled = false;
        }
        else if (switchData == 2)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = false;
            spaceCamera.enabled = true;
            greyCamera.enabled = false;
        }
        else if (switchData == 3)
        {
            oasisCamera.enabled = false;
            tropicCamera.enabled = false;
            spaceCamera.enabled = false;
            greyCamera.enabled = true;
        }
    }
}