using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Score : MonoBehaviour
{

    private DateTime startTime;
    private DateTime endTime;
    private TimeSpan currRunTime;

    private bool started = false;

    private float creepScore = 0;
    private float score;

    public GameObject textPopup_prefab;
    public GameObject textPopupPosition;
    public string[] scorePhrases = new string[] { "Mereço um café", "Hoje ou amanhã", "Envia-me um email", "Estudasses", "Pro' ano há mais", "Because why?" };

    public Dictionary<double, float> levelTimes = new Dictionary<double, float>();

    private void Awake()
    {
        LevelInfo levelInfo = GameObject.Find("Level Manager").GetComponent<LevelInfo>();
        if (levelInfo == null)
        {
            Debug.LogError("Level Manager is invalid. LevelInfo script missing.");
            return;
        }

        levelTimes = levelInfo.GetDict();
    }

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
        score = GetTimeScore() + creepScore + gameObject.GetComponent<PlayerHealth>().health;
    }

    //If the player takes too long the time score is 0
    float GetTimeScore()
    {
        double runTime = CurrentRunTime().TotalSeconds;

        foreach (KeyValuePair<double, float> entry in levelTimes){

            if(runTime < entry.Key)
            {
                return entry.Value;
            }
        }

        return 40;
        
    }

    public void IncrementScore(float amount)
    {
        creepScore += amount;

        TextPopup t = Instantiate(textPopup_prefab, textPopupPosition.transform.position, transform.rotation).GetComponent<TextPopup>();
        t.Setup(scorePhrases[Mathf.FloorToInt(Random.Range(0, scorePhrases.Length))]);
    }

    //Returns current run time
    //Uses endTime if it is set or current Time if endTime is not set
    TimeSpan CurrentRunTime()
    {

        DateTime tempTime;
        if (endTime == startTime)
        {
            tempTime = DateTime.Now;
        }
        else
        {
            tempTime = endTime;
        }

        return tempTime.Subtract(startTime);
    }

    private void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 100, 20), "Time: " + currRunTime.ToString(@"mm\:ss\:ff"));
        GUI.Label(new Rect(10, 30, 100, 20), "CreepScore: " + creepScore.ToString());
        GUI.Label(new Rect(10, 50, 200, 20), "Score Sums: " + GetTimeScore().ToString() + " "+ creepScore.ToString() + " " + gameObject.GetComponent<PlayerHealth>().health.ToString());

        //DEBUG ONLY
        if (GUI.Button(new Rect(10, 70, 70, 20), "D_Start")){
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
