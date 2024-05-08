using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float offset = 2f; 
    [SerializeField] private float followOffset = 2f; 

     
    private float boundary; 
    private bool followTarget = false; 

    void Start()
    {
        
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - offset;
    }

    void FixedUpdate()
    {
        
        if (target.position.x >= boundary)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            
            float distanceToRightEdge = Camera.main.WorldToViewportPoint(target.position).x;
            if (distanceToRightEdge >= (1 - followOffset))
            {
                followTarget = true;
            }
            else
            {
                followTarget = false; 
            }
        }

        
        if (followTarget)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}