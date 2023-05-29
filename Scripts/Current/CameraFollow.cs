using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5.0f;
    public float edgeBoundary = 50f; // Distance from edge of screen to start scrolling
    public float scrollSpeed = 5f; // Speed of scrolling

    private Vector3 offset;
    private bool isFollowingUnit;

    private float minX = -6.35f;
    private float maxX = 4.35f;
    private float minY = -2.92f;
    private float maxY = 2.85f;

    void Start()
    {
        offset = transform.position - target.position;
        isFollowingUnit = false; // Default to edge scrolling at the start
    }

    void Update()
    {
        if (!isFollowingUnit)
        {
            Vector3 newPosition = transform.position;

            // Check if we are on the edge of the screen horizontally
            if (Input.mousePosition.x > Screen.width - edgeBoundary)
            {
                newPosition.x += scrollSpeed * Time.deltaTime; // Move right
            }
            else if (Input.mousePosition.x < edgeBoundary)
            {
                newPosition.x -= scrollSpeed * Time.deltaTime; // Move left
            }

            // Check if we are on the edge of the screen vertically
            if (Input.mousePosition.y > Screen.height - edgeBoundary)
            {
                newPosition.y += scrollSpeed * Time.deltaTime; // Move up
            }
            else if (Input.mousePosition.y < edgeBoundary)
            {
                newPosition.y -= scrollSpeed * Time.deltaTime; // Move down
            }

            // Clamp the camera position so it stays within the game boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            // Set the camera's position
            transform.position = newPosition;
        }
    }

    void FixedUpdate()
    {
        if (target != null && isFollowingUnit)
        {
            Vector3 targetCamPos = target.position + offset;

            // Clamp the camera position so it stays within the game boundaries
            targetCamPos.x = Mathf.Clamp(targetCamPos.x, minX, maxX);
            targetCamPos.y = Mathf.Clamp(targetCamPos.y, minY, maxY);

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isFollowingUnit = true;
    }

    public void StopFollowingUnit()
    {
        isFollowingUnit = false;
    }
}
