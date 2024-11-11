using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 10f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isReturning = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 target = isReturning ? startPosition : targetPosition;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                isMoving = false;
                isReturning = !isReturning;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz wszedł na platformę.");

            other.gameObject.transform.parent = transform;

            if (!isMoving)
            {
                isMoving = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz zszedł z platformy.");

            other.gameObject.transform.parent = null;
        }
    }
}
