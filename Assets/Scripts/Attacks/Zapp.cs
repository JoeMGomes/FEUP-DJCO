﻿using UnityEngine;
using System.Collections;

public class Zapp : MonoBehaviour
{
    private Vector2 target;
    private LineRenderer lineRend;
    private float arcLength = 1.0f;
    private float arcVariation = 1.0f;
    public float inaccuracy = 0.1f;
    public float timeOfZap = 0.25f;
    private float zapTimer;
    private bool started;


    private Transform targetPlayer;
    private void Awake()
    {
    }
    void Start()
    {
        lineRend = gameObject.GetComponent<LineRenderer>();
        zapTimer = 0.25f;
        lineRend.positionCount = (1);
        targetPlayer = GameObject.Find("Player/Arm/Bullet_Spawn").GetComponent<Transform>();
        SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerShootPrimary);
    }

    void Update()
    {

        if (zapTimer > 0)
        {
            Vector2 lastPoint = targetPlayer.position;
            int i = 1;
            lineRend.SetPosition(0, targetPlayer.position);//make the origin of the LR the same as the transform
            while (Vector3.Distance(target, lastPoint) > 1.0f)
            {//was the last arc not touching the target?
                lineRend.positionCount = (i + 1);//then we need a new vertex in our line renderer
                Vector2 fwd = target - lastPoint;//gives the direction to our target from the end of the last arc
                fwd.Normalize();//makes the direction to scale
                fwd = Randomize(fwd, inaccuracy);//we don't want a straight line to the target though
                fwd *= Random.Range(arcLength * arcVariation, arcLength);//nature is never too uniform
                fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
                lineRend.SetPosition(i, fwd);//this tells the line renderer where to draw to
                i++;
                lastPoint = fwd;//so we know where we are starting from for the next arc
            }
            lineRend.positionCount = (i + 1);
            lineRend.SetPosition(i, target);
            zapTimer = zapTimer - Time.unscaledDeltaTime;
        }//If already fired
        else if(zapTimer <= 0 && started)
        {
            Destroy(gameObject);
        }
        else
            lineRend.positionCount = (1);

    }

    private Vector3 Randomize(Vector3 newVector, float devation)
    {
        newVector += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * devation;
        newVector.Normalize();
        return newVector;
    }

    public void ZapTarget(Vector2 newTarget)
    {
        target = newTarget;
        zapTimer = timeOfZap;
        started = true;
    }
}

