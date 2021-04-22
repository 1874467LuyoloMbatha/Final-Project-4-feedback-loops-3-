using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;

    public Text healthText;
    public Text blockText;
    public Text staminaText; 

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void displayUpdate() 
    {
        healthText.text = string.Format("{0}<color=#FFFFFF>/</color>{1}", combatManagerScript.instance.CUrrrentHealth,
            combatManagerScript.instance.maxHealth);
        blockText.text = string.Format("BLOCKED: {0}", combatManagerScript.instance.currentBlock);
        staminaText.text = string.Format("Mana: <color=#00FF00>{0}</color> / <color=#00FF00>{1}</color>",
            cardmanagerScript.instance.currentStamina, cardmanagerScript.instance.startStamina);
    }
}
