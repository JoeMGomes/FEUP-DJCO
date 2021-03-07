using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    private GameObject targetPlayer;
    public float health = 100;
    public float scoreValue = 20;
    public GameObject health_bar_prefab;
    private GameObject health_bar;
    private Slider health_slider;
    private Image health_fill;
    public Vector3 offset_healthbar = new Vector3(0,1,0);
    public Color high, low;
    private Rigidbody2D rigidBody;

    public GameObject textPopup_prefab;
    public GameObject textPopupPosition;

    public string[] hitPhrases = new string[] { "Mas...", "Oh stôree...", "Oh, mas..." };
    public string[] diePhrases = new string[] { "Vou falar com o pedagógico!", "Não fica assim...", "OK, desculpe-me" };

    private void Awake()
    {
        targetPlayer = GameObject.Find("Player");

        //Kind of messy but works fine
        health_bar = Instantiate(health_bar_prefab,  transform.position + offset_healthbar, transform.rotation);
        health_slider = health_bar.GetComponent<Slider>();
        health_slider.maxValue = health;
        health_slider.value = health;

        health_fill = health_slider.fillRect.GetComponentInChildren<Image>();
        health_bar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        health_bar.SetActive(false);

        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            targetPlayer.GetComponent<Score>().IncrementScore(scoreValue);
            TextPopup t = Instantiate(textPopup_prefab, textPopupPosition.transform.position, transform.rotation).GetComponent<TextPopup>();
            t.Setup(diePhrases[Mathf.FloorToInt(Random.Range(0, diePhrases.Length))]);
            Destroy(gameObject);
        }


        UpdateHealthBar();

    }

    void UpdateHealthBar()
    {
        
        health_slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset_healthbar );
        health_fill.color = Color.Lerp(low, high, health_slider.normalizedValue);
    }

    private void OnDestroy()
    {
        Destroy(health_bar);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health_slider.value = health;
        TextPopup t = Instantiate(textPopup_prefab, textPopupPosition.transform.position, transform.rotation).GetComponent<TextPopup>();
        t.Setup(hitPhrases[Mathf.FloorToInt(Random.Range(0,hitPhrases.Length))]);

    }

    public void Flinch(Vector3 hitpos, float force = 5)
    {
        float x = force;
        if (hitpos.x > transform.position.x)
            x = -x;

        rigidBody.AddForce(new Vector2(x, force), ForceMode2D.Impulse);
    }

    public void activeHealthbar(bool stat)
    {
        health_bar.SetActive(stat);
    }
}
