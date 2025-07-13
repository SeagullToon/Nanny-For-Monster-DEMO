using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_look_under_the_bed : MonoBehaviour
{
    public Trigger_Pumpkin trigger_Pumpkin;
    public ComicController comicController;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameStateManager.Instance.IsCutScene())
        {
            if (other.CompareTag("Player"))
            {
                if (GameStateManager.Instance.IsState(GameState.FollowMomo))
                {
                    Debug.Log("not active (under the bed)");

                }
                else if (GameStateManager.Instance.IsState(GameState.SearchSoundSource))
                {
                    if (trigger_Pumpkin.move_once)
                    {
                        Debug.Log("Look under the bed!!");
                        Debug.Log("--> the comics");
                        comicController.ShowComic__Kiss_Me();
                    }
                    else { Debug.Log("not active (under the bed)"); }
                }
                else if (GameStateManager.Instance.IsState(GameState.DemoEnd))
                {
                    Debug.Log("not active (under the bed)");
                }
            }
        }
    }
}

