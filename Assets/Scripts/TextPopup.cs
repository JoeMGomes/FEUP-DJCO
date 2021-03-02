using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{

    TextMeshPro textMesh;
    public float lifeTime= 2.0f;
    public float fadeSpeed = 0.5f;
    public Color textColor;
    public float MAX_MOVE_SPEED = 1.5f;
    public float MIN_MOVE_SPEED = 0.5f;
    private float moveSpeed_x;
    private float moveSpeed_y;
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(string msg)
    {
        textMesh.SetText(msg);
        textColor = textMesh.color;
        moveSpeed_x = Random.Range(-MAX_MOVE_SPEED, MAX_MOVE_SPEED);
        moveSpeed_y = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);
    }

    public void Update()
    {
        transform.position += new Vector3(moveSpeed_x, moveSpeed_y, 0) * Time.deltaTime;
        lifeTime -= Time.deltaTime;

        if(lifeTime < 0)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color= textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
