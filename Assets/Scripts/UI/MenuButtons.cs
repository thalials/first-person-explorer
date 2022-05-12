using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void PauseGame()
    {
        gm.ChangeState(GameManager.GameState.PAUSE);
    }

    public void BackToMainMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }

    public void PlayGame()
    {
        gm.ChangeState(GameManager.GameState.GAME);
    }

    public void GoToOptions()
    {
        gm.ChangeState(GameManager.GameState.OPTIONS);
    }

    public void GoToInstructions()
    {
        gm.ChangeState(GameManager.GameState.INSTRUCTIONS);
    }
}
