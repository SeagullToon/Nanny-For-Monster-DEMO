using System.Collections;
using UnityEngine;

/// <summary>
/// Управляет запуском всех катсцен в игре. Хранит ссылки на классы-катсцены.
/// Вызывается другими скриптами, чтобы запустить нужную сцену.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    // ===================== Катсцены =====================
    [Header("Ссылки на катсцены")]
    public StartCutscene startCutscene;

    // TODO: Добавляй сюда другие катсцены, например:
    // public PumpkinCutscene pumpkinCutscene;
    // public BedSceneCutscene bedSceneCutscene;

    // ===================== Запуск катсцены по имени =====================
    public static CutsceneManager Instance; // 👈 Этого не хватает

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void PlayCutscene(string cutsceneName)
    {
        GameStateManager.Instance.SetIsCutScene(true); // Блокируем геймплей

        switch (cutsceneName)
        {
            case "Start":
                StartCoroutine(RunCutscene(startCutscene.PlayIntro()));
                break;

            // TODO: Добавляй сюда новые кейсы для других сцен:
            // case "Pumpkin":
            //     StartCoroutine(RunCutscene(pumpkinCutscene.PlayPumpkinScene()));
            //     break;

            default:
                Debug.LogWarning("Катсцена с именем '" + cutsceneName + "' не найдена.");
                GameStateManager.Instance.SetIsCutScene(false); // Разблокируем геймплей, если катсцена не найдена
                break;
        }
    }

    // ===================== Обёртка для запуска корутин =====================

    /// <summary>
    /// Универсальная обёртка для запуска любой катсцены и автоматического снятия isCutScene в конце.
    /// </summary>
    private IEnumerator RunCutscene(IEnumerator cutsceneCoroutine)
    {
        yield return StartCoroutine(cutsceneCoroutine);
        GameStateManager.Instance.SetIsCutScene(false); // Разблокируем геймплей после завершения катсцены
    }
}
