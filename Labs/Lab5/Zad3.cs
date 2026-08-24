using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad3 : MonoBehaviour
{
    public List<Transform> pathPoints = new List<Transform>();
    private List<Vector3> targetPoints = new List<Vector3>();
    public float moveSpeed = 3.0f;
    private int currentPointIndex = 0;
    private bool movingForward = true;

    void Start()
    {
        targetPoints.Clear();
        targetPoints.Add(transform.position);
        foreach (Transform point in pathPoints)
        {
            targetPoints.Add(point.position);
        }
    }

    void Update()
    {
        if (targetPoints.Count == 0)
        {
            Debug.LogError("Błąd. Brak punktów");
            return;
        }

        Vector3 currentTarget = targetPoints[currentPointIndex];
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        if (transform.position == currentTarget)
        {
            if (movingForward)
            {
                currentPointIndex++;
                if (currentPointIndex >= targetPoints.Count)
                {
                    currentPointIndex = targetPoints.Count - 2;
                    movingForward = false;
                }
            }
            else
            {
                currentPointIndex--;
                if (currentPointIndex < 0)
                {
                    currentPointIndex = 1;
                    movingForward = true;
                }
            }
        }
    }
}
