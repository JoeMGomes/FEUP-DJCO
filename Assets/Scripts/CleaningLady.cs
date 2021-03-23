using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningLady : MonoBehaviour
{

    public string[] teacherReact = { "Passe por cima, sôtor!", "Cuidado num caia!" };
    public string[] studentReact = { "Tas tolo, moço??", "Bais tombar!!" };

    public GameObject textPopup;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TextPopup t = Instantiate(textPopup, transform.position, Quaternion.identity).GetComponent<TextPopup>();
            t.Setup(studentReact[Mathf.FloorToInt(Random.Range(0, studentReact.Length))]);
            StartCoroutine(IgnoreCollision(GetComponent<Collider2D>(), collision.collider));

        }
        if (collision.collider.CompareTag("Player"))
        {
            TextPopup t = Instantiate(textPopup, transform.position, Quaternion.identity).GetComponent<TextPopup>();
            t.Setup(teacherReact[Mathf.FloorToInt(Random.Range(0, teacherReact.Length))]);

        }
    }

    IEnumerator IgnoreCollision(Collider2D col1, Collider2D col2)
    {
        Physics2D.IgnoreCollision(col1, col2, true);
        while (Physics2D.Distance(col1, col2).isOverlapped) yield return null;
        Physics2D.IgnoreCollision(col1, col2, false);
    }

}
