using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Pumpkin : MonoBehaviour
{
    public PumpkinAnimator pumpkin;
    public bool move_once = false;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameStateManager.Instance.IsCutScene())
        {
            if (hasTriggered) return;

            if (other.CompareTag("Player"))
            {

                if (GameStateManager.Instance.IsState(GameState.FollowMomo))
                {
                    Debug.Log("Talk to Pump");

                }
                else if (GameStateManager.Instance.IsState(GameState.SearchSoundSource))
                {
                    if (!move_once)
                    {
                        Debug.Log("ask pumpkin to move away from bed");
                        //После этого тыковка уходит с места и освобождает триггер у кровати - "глянуть под кровать" 
                        // !Именно после запуска последнего триггера меняется состояние
                        
                        hasTriggered = true; // ставим флаг
                        StartCoroutine(PumpkinSequence());
                        Debug.Log(move_once);
                        // move_once = true;
                    }
                    else
                    {
                        Debug.Log("Talk to Pump");
                    }
                }
                else if (GameStateManager.Instance.IsState(GameState.DemoEnd))
                {
                    Debug.Log("Talk to Pump");
                }
            }
        }
    }

        private IEnumerator PumpkinSequence()
    {
        yield return pumpkin.PumpkinMovesAwayRoutine(); // дожидаемся конца анимации
    }
}