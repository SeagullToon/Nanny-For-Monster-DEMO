using UnityEngine;

public class ComicController : MonoBehaviour
{
    //Class-controller for controlling all the comics and answers
    //ShowComic__NAME_OF_COMIC
    public GameObject comicPanel;

    //Characters
    public MomoAnimator momo;
    public PumpkinAnimator pumpkin;
    
    public void ShowComic__Kiss_Me()
    {
        comicPanel.SetActive(true);
        Debug.Log("Comic__Kiss_Me.. kiss, notkiss, forehead");
        ChooseAnswer__Kiss_Me(0);
    }
    public void ChooseAnswer__Kiss_Me(int index)
    {
        Debug.Log("Выбран вариант: " + index);
        // Заглушка разной реакции
        GameStateManager.Instance.SetState(GameState.DemoEnd);
        momo.SetIdle();
        momo.Momo_DemoEnd();
        pumpkin.Pumpkin_DemoEnd();

    }
}
