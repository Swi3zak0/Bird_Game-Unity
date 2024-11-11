using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad4 : MonoBehaviour
{
    public float launchForce = 15.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 launchVector = Vector3.up * launchForce;
                playerRigidbody.AddForce(launchVector, ForceMode.VelocityChange);
                Debug.Log("Wyrzucenie");
            }
        }
    }
}
