using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject targetPlayer;
    public float chaseDistance = 5.0f; //Distance at wich enemies will start to follow the player
    public float moveSpeed = 3.0f;


    private void Awake()
    {
        targetPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = transform.position - targetPlayer.transform.position;
        if (direction.magnitude < chaseDistance)
        {
           
            transform.Translate(moveSpeed * Time.deltaTime * (direction.x < 0 ? 1 : -1), 0f, 0f);
        }
    }
}
