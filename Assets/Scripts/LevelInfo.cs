using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    //These arrays must be the same size as they will be converted to a Dictionary
    //Having seperate arrays helps with Unity's visualization in Editor
    public double[] timeGoals = { 60,90,140};
    public float[] scoreValues= { 200, 150, 50};

    // Start is called before the first frame update
    void Start()
    {
        if(timeGoals.Length != scoreValues.Length)
        {
            Debug.LogError("Invalid time and score arrays. They need to be the same size");
        }
    }

    public Dictionary<double, float> GetDict()
    {
        Dictionary<double, float> dict = new Dictionary<double, float>();

        for(int i = 0; i < timeGoals.Length; i++)
        {
            dict.Add(timeGoals[i], scoreValues[i]);
        }

        return dict;
    }

 
}
