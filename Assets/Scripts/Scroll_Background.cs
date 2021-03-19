using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scroll_Background : MonoBehaviour
{

    public float speed = 1.0f;
    private MeshRenderer rend;
    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        rend.material.SetTextureOffset("_MainTex", new Vector2(Time.time * speed, 0f));
    }
}
