using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameState
    {
        MENU,
        GAME,
        PAUSE,
        ENDGAME
    }

    public TimeHandler timer;

    public GameState gameState { get; private set; }

    private static GameManager _instance;

    public delegate void ChangeStateDelegate();

    public static ChangeStateDelegate changeStateDelegate;

    public void ChangeState(GameState nextState)
    {
        if (
            (gameState == GameState.ENDGAME && nextState == GameState.GAME) ||
            (gameState == GameState.ENDGAME && nextState == GameState.MENU) ||
            (gameState == GameState.PAUSE && nextState == GameState.MENU)
        )
        {
            Reset();
        }

        Debug.Log($"NextState: {nextState}");

        if (nextState == GameState.GAME)
        {
            LockCursor();
            Time.timeScale = 1;
        }
        else
        {
            UnlockCursor();
            Time.timeScale = 0;
        }

        gameState = nextState;
        changeStateDelegate();
    }

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }

    private GameManager()
    {
        gameState = GameState.MENU;
        timer = new TimeHandler(this);
        Time.timeScale = 0;
        Reset();
    }

    public void Reset()
    {
        timer.Reset();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
