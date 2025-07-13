using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_sit_on_a_bed : MonoBehaviour
{
    
    public DialogueController dialogueController;

    public DialogueLine[] KnockKnock_Dialog;


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (!other.CompareTag("Player")) return;

        if (GameStateManager.Instance.IsState(GameState.FollowMomo))
        {
            Debug.Log("Игрок sits on a bed — запускаем диалог с Момо");
            // Здесь можно вызывать диалог и затем смену состояния:
            
            dialogueController.StartDialogue(KnockKnock_Dialog);

            GameStateManager.Instance.SetState(GameState.SearchSoundSource);
            
        }
        else if (GameStateManager.Instance.IsState(GameState.SearchSoundSource))
        {
            Debug.Log("can't sit again??");

        }
        else if (GameStateManager.Instance.IsState(GameState.DemoEnd))
        {
            Debug.Log("can't sit again??");
        }
    }
}
