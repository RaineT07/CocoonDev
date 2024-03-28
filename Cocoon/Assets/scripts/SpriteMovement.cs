using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    [Header("Sprite Movement Path")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private float speed;


    public float easingFactor = 2f;
   
    public enum PathType
    {
        Straight,
        Curved,
        Spiral,
        Sin
    };

    public enum TravelType
    {
       Horizontal,
       Vertical,
       Travel,
       None
    };


    private int currentTargetIndex = 0;
    private bool isFacingRight = false;
    public SpriteRenderer spriteRenderer;



    public enum xAlign { Left, Center, Right, Whole};
    public enum yAlign {Top, Center, Bottom, Whole};

    public xAlign HorizontalAlignment;
    public yAlign VerticalAlignment;
    public TravelType travelType;

    public PathType pathType;

    public Camera sceneCamera;

    public int targets = 4;
    private Vector3[] targetPositions;// = new Vector3[targets]; // 4 target positions
   // public bool isTraveling;
    float count = 0.0f;

    

    void Start()
    {

        

        targetPositions = new Vector3[targets];
        speed = Random.Range(minSpeed, maxSpeed);

        if(travelType==TravelType.Travel ) {
            for (int i = 0; i < targetPositions.Length; i++)
            {
                targetPositions[i] = GetRandomScreenPosition();
            }
        }

        
        transform.position = targetPositions[currentTargetIndex];


      

    }

    // Update is called once per frame
    void Update()
    {
        if(pathType==PathType.Straight) {
            StraightLine();
        }
        if (pathType == PathType.Curved)
        {
            CurvedLine();
        }




    }

    Vector3 GetRandomScreenPosition()
    {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);

        // Change later to specific cameras
        return Camera.main.ViewportToWorldPoint(new Vector3(x, y, Random.Range(300, 700)));
    }

    void StraightLine() {
        // Calculate the distance to the current target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPositions[currentTargetIndex]);
        float easedSpeed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.SmoothStep(0f, 1f, distanceToTarget / easingFactor));
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentTargetIndex], easedSpeed * Time.deltaTime);

        
        if (distanceToTarget < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
        }
    }

    void CurvedLine() //admittingly cant tell if this worked
    {
        Vector3 lastTarget = currentTargetIndex > 0 ? targetPositions[currentTargetIndex - 1] : transform.position; // Use initial position if currentTargetIndex is 0
        Vector3 midway = (lastTarget + targetPositions[currentTargetIndex]) / 2f + Vector3.Cross(targetPositions[currentTargetIndex] - lastTarget, Vector3.forward).normalized * 2.5f;

        if (count < 1.0f)
        {
            count += Time.deltaTime * speed / Vector3.Distance(lastTarget, targetPositions[currentTargetIndex]);

            Vector3 m1 = Vector3.Lerp(lastTarget, midway, count);
            Vector3 m2 = Vector3.Lerp(midway, targetPositions[currentTargetIndex], count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        else
        {
            count = 0.0f; // Reset count for the next curve segment
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
        }
    }

    Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1-t)^3 * p0
        p += 3 * uu * t * p1; // 3*(1-t)^2 * t * p1
        p += 3 * u * tt * p2; // 3*(1-t) * t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }

    void SinLine() { }

    void UpdateFacingDirection()
    {
        Vector3 currentDirection = (targetPositions[currentTargetIndex] - transform.position).normalized;
        isFacingRight = currentDirection.x > 0;
        spriteRenderer.flipX = isFacingRight; // Flip the sprite based on the current facing direction
        //previousDirection = currentDirection;
        Debug.Log(isFacingRight);
    }

    // Check the initial facing direction based on the first movement
    void CheckInitialFacingDirection()
    {
        Vector3 currentDirection = (targetPositions[currentTargetIndex] - transform.position).normalized;
        isFacingRight = currentDirection.x > 0;
        spriteRenderer.flipX = isFacingRight; // Flip the sprite based on the initial facing direction
       // previousDirection = currentDirection;
        Debug.Log(isFacingRight);
    }



}
