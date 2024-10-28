using UnityEngine;

public class SmoothDampFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private float yVelocity = 0.0f;

    void Update()
    {
        float newPositionY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
    }
}
