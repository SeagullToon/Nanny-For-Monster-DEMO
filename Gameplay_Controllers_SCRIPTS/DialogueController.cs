// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// [System.Serializable]
// public class DialogueLine
// {
//     public string speakerName;
//     [TextArea(2, 5)]
//     public string text;
// }

// public class DialogueController : MonoBehaviour
// {
//     public GameObject dialogueBox;          // панель диалога
//     public TMP_Text speakerNameText;        // текст имени
//     public TMP_Text dialogueText;           // текст реплики
//     public RectTransform bubbleTransform;   // трансформ пузыря (чтобы растягивать)

//     public DialogueLine[] lines;            // список строк
//     private int currentLine = 0;
//     private bool isActive = false;

//     void Start()
//     {
//         dialogueBox.SetActive(false); // скрыть диалог в начале
//     }

//     void Update()
//     {
//         if (isActive && Input.GetKeyDown(KeyCode.Space)) // нажми пробел для продолжения
//         {
//             NextLine();
//         }
//     }

//     public void StartDialogue(DialogueLine[] dialogue)
//     {
//         lines = dialogue;
//         currentLine = 0;
//         isActive = true;
//         dialogueBox.SetActive(true);
//         ShowLine();
//     }

//     void ShowLine()
//     {
//         DialogueLine line = lines[currentLine];
//         speakerNameText.text = line.speakerName;
//         dialogueText.text = line.text;

//         // Авто-настройка размеров пузыря
//         LayoutRebuilder.ForceRebuildLayoutImmediate(bubbleTransform);
//     }

//     void NextLine()
//     {
//         currentLine++;
//         if (currentLine < lines.Length)
//         {
//             ShowLine();
//         }
//         else
//         {
//             EndDialogue();
//         }
//     }

//     void EndDialogue()
//     {
//         dialogueBox.SetActive(false);
//         isActive = false;
//     }
// }




///
using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public GameObject player; // перс, у которого есть PlayerController


    public void StartDialogue__KnockKnock()
    {
        StartCoroutine(DialogueRoutine__KnockKnock());
    }

    private IEnumerator DialogueRoutine__KnockKnock()
    {
        // Отключить управление
        player.GetComponent<ElliottMovement>().canMove = false;

        // Показать диалог (заглушка)
        Debug.Log("Диалог: тук-тук");


        yield return new WaitForSeconds(2f); // подожди 2 секунды

        
        GameStateManager.Instance.SetState(GameState.SearchSoundSource);
        Debug.Log("OBJ: find noise sourse");

        // Вернуть управление
        player.GetComponent<ElliottMovement>().canMove = true;
    }
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName;  // Кто говорит
    public string text;         // Что говорит

    DialogueLine[] momoDialog = new DialogueLine[]

    {
        new DialogueLine { speakerName = "Momo", text = "Слышишь?" },
        new DialogueLine { speakerName = "Elliott", text = "Не слышу ничего. Как оно звучит?" }
    };

    FindObjectOfType<DialogueController>().StartDialogue(momoDialog);
}
