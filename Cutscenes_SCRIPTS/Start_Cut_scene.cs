using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public GameObject player;
    public float delayBeforeControl = 0.1f;
    public MomoAnimator momo; // привязывается в инспекторе
    public PumpkinAnimator pumpkin;

    public System.Collections.IEnumerator PlayIntro()
    {
        GameStateManager.Instance.SetIsCutScene(true);
        Debug.Log("is cut scene = "+GameStateManager.Instance.IsCutScene());
        // Запустить анимации зевка, чесания и т.д. (если нужно)
        Debug.Log("Катсцена: Эллиотт зевает, Тыковка чешется, Момо убегает");

        // Блокируем управление
        player.GetComponent<ElliottMovement>().canMove = false;

        // Запустить анимацию Момо "убегает"
        // или просто сдвинуть за пределы экрана
        // где-то внутри StartCoroutine или другого метода
        momo.MoveToPosition(new Vector3(6.53f, -3.4f, 0.1f), 1.5f); // двигаем за 2 секунды
        yield return new WaitForSeconds(1f);
        pumpkin.MoveToPosition(new Vector3(3.7f, -4.34f, -0.6f), 3.5f);
        yield return new WaitForSeconds(0.5f);
        momo.MoveCustomSprite(new Vector3(6.97f, -0.5f, -0.5f), 0.1f);
        yield return new WaitForSeconds(0.1f);

        // Включаем управление
        player.GetComponent<ElliottMovement>().canMove = true;

        Debug.Log("Игрок получил контроль");
        yield return new WaitForSeconds(2f);
        GameStateManager.Instance.SetIsCutScene(false);
        Debug.Log("is cut scene = "+GameStateManager.Instance.IsCutScene());
    }
}
