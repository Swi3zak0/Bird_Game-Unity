using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad5 : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Przeszkoda"))
        {
            Debug.Log("Uderzenie z przeszkodą!");
        }
    }
}
