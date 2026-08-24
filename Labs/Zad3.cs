using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePathMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 target;

    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        target = transform.position + transform.forward * 10;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            transform.Rotate(new Vector3(0f, 90f, 0f), Space.Self);
            target = transform.position + transform.forward * 10;
        }
    }
}
