using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInstancing : MonoBehaviour
{
    [SerializeField]
    public GameObject[] OasisSkySprites;
    public Camera OasisCam;
    void Start()
    {
        Instantiate(OasisSkySprites[0], new Vector3(0, 0, 300), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //spawn new butterflyon space press
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Vector3 randomSpawnPos = new Vector3(Random.Range(-100,300), Random.Range(-10, 200), Random.Range(300, 700));
            Vector3 screenPosition = OasisCam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Random.Range(300, 700)));
            Instantiate(OasisSkySprites[0], screenPosition, Quaternion.identity);
        }
        
    }
}

