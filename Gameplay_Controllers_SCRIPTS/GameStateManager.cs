using UnityEngine;

public enum GameState
{
    FollowMomo,
    SearchSoundSource,
    DemoEnd
}


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    // public bool IsCutScene { get; set; }
    public bool isCutScene;

    public GameState currentState = GameState.FollowMomo;
    public MomoDragController momoDrag;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        CutsceneManager.Instance.PlayCutscene("Start");
    }


    public void SetIsCutScene(bool value)
    {
        isCutScene = value;
    }

    public bool IsCutScene()
    {
        return isCutScene;
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Состояние изменено на: " + currentState);
        if (newState == GameState.DemoEnd)
        {
            momoDrag.EnableDrag();
            Debug.Log("EnableDrag called "+momoDrag.enabled);
        }

    }

    public bool IsState(GameState state)
    {
        return currentState == state;
    }



}
