using UnityEngine;

public class LerpFollower : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 5f;

    void Update()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}
