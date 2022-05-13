using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler
{
    public int minutes = 0;

    public int seconds = 0;

    public int timeLimit = 60;

    private float _lastCheck = 0.0f;

    private GameManager gm;

    public TimeHandler(GameManager gameManagerInstance)
    {
        this.gm = gameManagerInstance;
    }

    public void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;

        float now = Time.time;
        int deltaTime = (int)(now - _lastCheck);

        if (deltaTime >= 1)
        {
            seconds++;

            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }

            _lastCheck = Time.time;
        }

        if (TimeLimitReached())
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }

    public bool TimeLimitReached()
    {
        return minutes * 60 + seconds > timeLimit;
    }

    public void Reset()
    {
        minutes = 0;
        seconds = 0;
        _lastCheck = 0.0f;
    }
}
