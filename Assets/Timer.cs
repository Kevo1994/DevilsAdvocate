using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Evaluator eval;
    public Image clockFace;

    public float TimerLength;

    private float timeRemaining;

    private float percentage;

    bool timesUp = false;

    // Use this for initialization
    void Start() {
        timeRemaining = TimerLength;
        clockFace.fillAmount = 0;

    }

    // Update is called once per frame
    void Update() {

        if (!timesUp)
        {
            timeRemaining -= Time.deltaTime;

            percentage = (TimerLength - timeRemaining) / TimerLength;
            clockFace.fillAmount = percentage;
        }


    }

    void TimesUp()
    {

        timesUp = true;
        eval.check();


    }
}
