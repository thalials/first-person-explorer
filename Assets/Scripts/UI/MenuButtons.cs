using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    GameManager gm;

    public GameObject Settings;

    public GameObject Instructions;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void PlayGame()
    {
        gm.ChangeState(GameManager.GameState.GAME);
        Settings.SetActive(false);
        Instructions.SetActive(false);
    }

    public void GoToMainMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }

    public void GoToPauseMenu()
    {
        gm.ChangeState(GameManager.GameState.PAUSE);
    }

    public void OpenSettings()
    {
        Settings.SetActive(true);
    }

    public void OpenInstructions()
    {
        Instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        Instructions.SetActive(false);
    }

    public void CloseSettings()
    {
        Settings.SetActive(false);
    }
}
