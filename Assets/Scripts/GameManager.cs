using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public enum GameState
    {
        MENU,
        GAME,
        PAUSE,
        OPTIONS,
        INSTRUCTIONS,
        ENDGAME
    }

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
            // restart game
        }

        Debug.Log($"nextState: {nextState}");

        if (nextState == GameState.GAME)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
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
        this.Setup();
    }

    private void Setup()
    {
        gameState = GameState.MENU;
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
