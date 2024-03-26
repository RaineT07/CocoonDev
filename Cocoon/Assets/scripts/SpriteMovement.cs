using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    [Header("Sprite Movement Path")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public enum PathType { Straight, Curved, Spiral };

    private int currentTargetIndex = 0;
    private bool isFacingRight = false;


    //private Vector3[] targetPositions;// = new Vector3[targets];
    public enum xAlign { Left, Center, Right, Whole};
    public enum yAlign {Top, Center, Bottom, Whole };

    public xAlign HorizontalAlignment;
    public yAlign VerticalAlignment;

    public Camera sceneCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomScreenPosition()
    {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);

        // Change later to specific cameras
        return Camera.main.ViewportToWorldPoint(new Vector3(x, y, Random.Range(300, 700)));
    }
}
