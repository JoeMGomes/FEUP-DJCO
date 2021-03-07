using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
