using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door");
        }
    }
}
