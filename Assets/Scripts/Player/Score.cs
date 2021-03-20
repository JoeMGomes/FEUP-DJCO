using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Score : MonoBehaviour
{

    private float runTime = 0;

    private bool started = false;

    private float creepScore = 0;
    private float score;

    public GameObject textPopup_prefab;
    public GameObject textPopupPosition;
    public string[] scorePhrases = new string[] { "Mereço um café", "Hoje ou amanhã", "Envia-me um email", "Estudasses", "Pro' ano há mais", "Because why?" };

    public Dictionary<double, float> levelTimes = new Dictionary<double, float>();

    public float RunTime { get => runTime; set => runTime = value; }
    public float TotalScore { get => score; set => score = value; }

    private void Awake()
    {
        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("Level Manager is invalid. LevelManager script missing.");
            return;
        }

        levelTimes = levelManager.GetDict();
    }

    void Update()
    {
        if (started)
        {
            runTime += Time.deltaTime;
        }
    }

    public void StartRun()
    {
        started = true;
        runTime = 0;
        score = 0;
    }

    public void EndRun()
    {
        started = false;
        score = GetTimeScore() + creepScore + gameObject.GetComponent<PlayerHealth>().health;
    }

    public float GetScore() {
        return score;
    }

    //If the player takes too long the time score is 0
    float GetTimeScore()
    {

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


    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Time: " + TimeSpan.FromSeconds(runTime).ToString("mm\\:ss\\.ff"));
        GUI.Label(new Rect(10, 30, 100, 20), "CreepScore: " + creepScore.ToString());
        GUI.Label(new Rect(10, 50, 200, 20), "Score Sums: " + GetTimeScore().ToString() + " " + creepScore.ToString() + " " + gameObject.GetComponent<PlayerHealth>().health.ToString());

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
