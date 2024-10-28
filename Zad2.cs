using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Zad2 : MonoBehaviour
{
    public float speed = 2.0f;
    public bool moving = true;
    void Start()
    {
        transform.position = new Vector3(0f,0f,0f);
    }
    void Update()
    {
        if(moving == true){
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if(transform.position.x >= 10){
                moving = false;
            }
        }
        else{
            transform.Translate(Vector3.left * speed * Time.deltaTime);
             if(transform.position.x <= 0){
                moving = true;
             }
        }
    }
}