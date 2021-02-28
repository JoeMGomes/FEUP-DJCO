using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{

    public float mana;
    public float maxMana = 100f;
    public float manaPerSecond = 10f;

    public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        mana += manaPerSecond * Time.deltaTime;
        if (mana > maxMana) mana = maxMana;
        manaBar.SetMana(mana);
    }

    public void UseMana(float usedMana)
    {
        mana -= usedMana;
        manaBar.SetMana(mana);
    }
}
