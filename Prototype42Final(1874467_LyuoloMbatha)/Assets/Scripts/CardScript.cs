using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public cardDealScript cardData;

    [Header("Card info:")]
    public Text cardNameT;
    public Text cardDescriptionText;
    public Text cardTypeText;
    public Text cardStaminaText;

    [Header("Card Data:")]
    public string cardName;
    public string cardDesc;
    public CardTypesScript cardType;
    public int cardStamina;

    public int strength;
    public int defense;
    public int cardDraw;

    private void Start()
    {
        CollectInfoCardSO();
    }

    private void CollectInfoCardSO() 
    {
        if (cardData == null) 
        {
            Destroy(this.gameObject);
            return;
        }
        cardName = cardData.cardName;
        cardDesc = cardData.cardDescript;
        cardType = cardData.type;
        cardStamina = cardData.cardStamina;
        strength = cardData.strength;
        defense = cardData.defensive;
        cardDraw = cardData.drawCardAmmount;

        displayUpdate();
    }

    public void displayUpdate() 
    {
        cardNameT.text = cardName;
        cardDesc = cardData.cardDescript;
        cardDesc = processedDescript();
        cardDescriptionText.text = cardDesc;
        cardTypeText.text = cardType.ToString().ToUpper();
        cardStaminaText.text = cardStamina.ToString();
    }

    private string processedDescript() 
    {
        string[] temp = cardDesc.Split(' ');
        string newCardDesc = "";
        cardDesc = "";

        switch (cardData.type)
        {
            case CardTypesScript.ATTACK:
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].ToUpper() == "X") 
                    {
                        temp[i] = strength.ToString();
                    }

                    newCardDesc += temp[i];
                    if (i < temp.Length - 1)
                        newCardDesc += " ";
                }
                break;
                

            case CardTypesScript.DEFENSE:
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].ToUpper() == "X")
                    {
                        temp[i] = defense.ToString();
                    }

                    newCardDesc += temp[i];
                    if (i < temp.Length - 1)
                        newCardDesc += " ";
                }
                break; 

            case CardTypesScript.ITEM:
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].ToUpper() == "X")
                    {
                        temp[i] = cardDraw.ToString();
                    }

                    newCardDesc += temp[i];
                    if (i < temp.Length - 1)
                        newCardDesc += " ";
                }
                break;

            case CardTypesScript.BUFF:
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].ToUpper() == "X")
                    {
                        temp[i] = cardDraw.ToString();
                    }

                    newCardDesc += temp[i];
                    if (i < temp.Length - 1)
                        newCardDesc += " ";
                }
                break; 
        }
        return newCardDesc;
    }

    public void CardUse() 
    {
        if (gameManagerScript.instance.currentStates == GameStatess.ENDMATCH)
        {
            //resets combat phase
            //adds another card to deck pile.


        }
        else if (gameManagerScript.instance.currentStates == GameStatess.COMBAT) 
        {
            if (cardmanagerScript.instance.canUseC(this)) 
            {
                cardmanagerScript.instance.currentStamina -= cardStamina;

                switch (cardType)
                {
                    case CardTypesScript.ATTACK:
                        combatManagerScript.instance.Attack(strength, this);
                        break;

                    case CardTypesScript.DEFENSE:
                        combatManagerScript.instance.addDefence(defense, this);

                        if (cardDraw > 0)
                            StartCoroutine(cardmanagerScript.instance.DrawCardss(this));
                        break;

                    case CardTypesScript.ITEM:
                        if(cardDraw > 0 )
                            StartCoroutine(cardmanagerScript.instance.DrawCardss(this));
                        break;

                    case CardTypesScript.BUFF:
                        break;

                }

                UIManagerScript.instance.displayUpdate();

            }
        
        }

    } 
}
