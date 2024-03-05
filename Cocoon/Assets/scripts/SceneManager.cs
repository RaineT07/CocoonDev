using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // [SerializeField]
    // public ArduinoListener arduinoListener;
    public SceneSet sceneSet;
    public GameObject[] layers;
    public GameObject ArduinoListener;


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<layers.Length; i++)
        {
            layers[i].GetComponent<Renderer>().material.mainTexture = sceneSet.textures[i];
            layers[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
