using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text timeLabel;

    TimeHandler timer;

    GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();
        timer = gm.timer;
    }

    void Update()
    {
        timer.Update();
        UpdateTime();
    }

    void UpdateTime()
    {
        int remainingSeconds =
            Math.Max(timer.timeLimit - timer.minutes * 60 - timer.seconds, 0);

        int seconds;
        float minutes = Math.DivRem(remainingSeconds, 60, out seconds);

        if (seconds >= 10)
        {
            timeLabel.text = $"0{minutes}:{seconds}";
        }
        else
        {
            timeLabel.text = $"0{minutes}:0{seconds}";
        }
    }
}
