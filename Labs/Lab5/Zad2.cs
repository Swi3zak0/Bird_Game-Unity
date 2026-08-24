using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad2 : MonoBehaviour
{   
    private bool isPlayerOn = false;
    private bool isOpening = false;
    private bool isClosing = false;

    void Update()
    {   
        if (isPlayerOn)
        {   
            
            if (isOpening) {
                transform.Translate(Vector3.right * Time.deltaTime);

                if(transform.position.x >= 5){
                    isOpening = false;
                }
            }
        }

        if(!isPlayerOn && isClosing) {
            transform.Translate(Vector3.left * Time.deltaTime);
            if (transform.position.x <= 0) 
            {
                isClosing = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            isPlayerOn = true;
            isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            isClosing = true;
        }
    }
}