using UnityEngine;

public class Trigger_chimney : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("chimney");
        }
    }
}
