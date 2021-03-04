using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform cameraPos;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos.position = new Vector3(player.transform.position.x, cameraPos.position.y, cameraPos.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos.position = new Vector3(player.transform.position.x, cameraPos.position.y, cameraPos.position.z);
    }
}
