using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_push_cupboard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameStateManager.Instance.IsState(GameState.FollowMomo))
            {
                Debug.Log("not active");

            }
            else if (GameStateManager.Instance.IsState(GameState.SearchSoundSource))
            {
                Debug.Log("Push the cubboard");

            }
            else if (GameStateManager.Instance.IsState(GameState.DemoEnd))
            {
                Debug.Log("Not active");
            }
        }
    }
}
