using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float minSpeed = 1f; 
    public float maxSpeed = 3f;
    public float easingFactor = 2f; 
    public SpriteRenderer spriteRenderer;

    public int targets = 4;

    private float speed;
    private Vector3[] targetPositions = new Vector3[4]; // 4 target positions
    private int currentTargetIndex = 0;
    private Vector3 previousDirection;
    private bool isFacingRight = false;

    void Start()
    {
        //targetPositions = new Vector3[targets];
        // Randomize the speed of the butterfly
        speed = Random.Range(minSpeed, maxSpeed);

        // Choose 4 random target positions within the camera bounds
        for (int i = 0; i < targetPositions.Length; i++)
        {
            targetPositions[i] = GetRandomScreenPosition();
        }

        transform.position = targetPositions[currentTargetIndex];
        CheckInitialFacingDirection();
    }

    void Update()
    {
        // Calculate the distance to the current target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPositions[currentTargetIndex]);
        float easedSpeed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.SmoothStep(0f, 1f, distanceToTarget / easingFactor));
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentTargetIndex], easedSpeed * Time.deltaTime);

        // If the butterfly reaches the current target position, cycle to the next target
        if (distanceToTarget < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
            UpdateFacingDirection();
        }
    }

    // Function to get a random position within the camera bounds
    Vector3 GetRandomScreenPosition()
    {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);

        // Change later to specific cameras
        return Camera.main.ViewportToWorldPoint(new Vector3(x, y, Random.Range(300, 700)));
    }

    // Check and update the facing direction based on the current movement direction
    void UpdateFacingDirection()
    {
        Vector3 currentDirection = (targetPositions[currentTargetIndex] - transform.position).normalized;
        isFacingRight = currentDirection.x > 0;
        spriteRenderer.flipX = isFacingRight; // Flip the sprite based on the current facing direction
        previousDirection = currentDirection;
        Debug.Log(isFacingRight);
    }

    // Check the initial facing direction based on the first movement
    void CheckInitialFacingDirection()
    {
        Vector3 currentDirection = (targetPositions[currentTargetIndex] - transform.position).normalized;
        isFacingRight = currentDirection.x > 0;
        spriteRenderer.flipX = isFacingRight; // Flip the sprite based on the initial facing direction
        previousDirection = currentDirection;
        Debug.Log(isFacingRight);
    }
}
