using UnityEngine;

public class Trigger_cupboard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("whats in the cupboard");
        }
    }
}
