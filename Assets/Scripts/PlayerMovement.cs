using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D m_rigidbody;
    public float moveSpeed = 3.0f;
    public float jumpHeight = 7.0f;
    public float groundDist;
    public LayerMask floorLayer;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        groundDist = GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            m_rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {   //Shoots a raycast to the ground 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDist+0.2f, floorLayer);
        //Returns true if the rayvast hit an object with floorLayer Layer
        return hit.collider != null;
    }
}
