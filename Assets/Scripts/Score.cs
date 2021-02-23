using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    private DateTime startTime;
    private DateTime endTime;
    private TimeSpan currRunTime;

    private bool started = false;

    private float score = 34;

    void Update()
    {
        if (started)
        {
            currRunTime = CurrentRunTime();
        }
    }

    void StartRun()
    {
        started = true;
        startTime = System.DateTime.Now;
        endTime = System.DateTime.Now;
        score = 0;
    }

    void EndRun()
    {
        started = false;
        endTime = System.DateTime.Now;
        score = (100000000.0f / (float)CurrentRunTime().TotalMilliseconds) + score;
    }

    void IncrementScore(float amount)
    {
        score += amount;
    }

    //Returns current run time
    //Uses endTime if it is set or current Time if endTime is not set
    TimeSpan CurrentRunTime()
    {

        DateTime tempTime;
        if (endTime == startTime)
        {
            tempTime = System.DateTime.Now;
        }
        else
        {
            tempTime = endTime;
        }

        return (TimeSpan)tempTime.Subtract(startTime);
    }

    private void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 100, 20), "Time: " + currRunTime.ToString(@"mm\:ss\:ff"));
        GUI.Label(new Rect(10, 30, 100, 20), "Score: " + score.ToString());

        //DEBUG ONLY
        if(GUI.Button(new Rect(10, 70, 70, 20), "D_Start")){
            StartRun();
        }
        if (GUI.Button(new Rect(10, 90, 70, 20), "D_End")){
            EndRun();
        }
        if (GUI.Button(new Rect(10, 110, 80, 20), "D_Score"))
        {
            IncrementScore(20);
        }
    }
}
