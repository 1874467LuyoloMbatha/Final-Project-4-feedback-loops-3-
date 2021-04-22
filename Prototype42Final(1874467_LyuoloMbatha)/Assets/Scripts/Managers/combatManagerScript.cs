using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatManagerScript : MonoBehaviour
{
    public static combatManagerScript instance;

    public EnemyScript currentOpps;

    public int startHealth = 50;
    public int currentBlock = 0;
    public int currentMite = 0;

    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int CUrrrentHealth
    { get 
        { 
            return currentHealth; 
        } 
        set 
        {
            currentHealth = value;
            updateHealthDis();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        maxHealth = startHealth;
        CUrrrentHealth = maxHealth;

        UIManagerScript.instance.displayUpdate();
    }

    private void updateHealthDis() 
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UIManagerScript.instance.displayUpdate();
    }

    public void Attack(int d, CardScript card) 
    {
        //facilitates damage.
        if (currentOpps != null)
            currentOpps.TakeDmg(d);

        if (card != null)
            cardmanagerScript.instance.dicardCard(card);

        cardmanagerScript.instance.UpdateDisplay();
    }

    public void addDefence(int d, CardScript card) 
    {
        currentBlock += d;

        if (card != null)
            cardmanagerScript.instance.dicardCard(card);

        cardmanagerScript.instance.UpdateDisplay();
    }

    public void TakeDmg(int d) 
    {
        int dmg = d;

        if (currentBlock >= dmg)
            currentBlock -= dmg;
        else 
        {
            dmg -= currentBlock;
            currentBlock = 0;

            currentHealth -= dmg;
        }
    }
    public void Heall(int d) 
    {
        currentHealth += d;
    
    }


}
