using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardmanagerScript : MonoBehaviour
{
    public static cardmanagerScript instance;

    #region Variables
    public currentTurnScript currentTurn;

    public GameObject newCardPre;

    public Transform drawContainment; // will contain cards in draw pile
    public Transform discardContainment; // will contain cards in discard pile
    public Transform cardHolder; // show cards player is holdin

    [Header("Cards in Current Deck:")]
    //cards currently in deck
    public List<cardDealScript> cAvaliableCards = new List<cardDealScript>();

   [Header("All Card in Game:")]
    public List<cardDealScript> allCardsIG = new List<cardDealScript>();

    [Header("Hand Size:")]
    public int startingHand = 4;
    public int maxHand = 6;

    [Header("Stamina:")]
    //Stamina at tHe start of a turn 
    public int startStamina = 3;
    //Current stamina left durin turn 
    public int currentStamina = 3;

    public bool isDrawStarting = false;

    [Header("User Interface:")]
    //cards left in draw pile
    public Text drawCardText;
    // amount of cards in discard pile
    public Text discardCardText;

    // end turn button reference
    public Button endTurnB;
    #endregion

    private void Awake()
    {
        // Destroys all objects with this script attached to it in a new scene
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        isDrawStarting = true; 
        deckLoad();
        
    }

    //Occurs at start of combat.
    public void deckLoad() 
    {
        for (int i = 0; i < cAvaliableCards.Count; i++)
        {
            GameObject g = Instantiate(newCardPre, drawContainment);

            //sets card to cardDealScript for cloned prefab
            g.GetComponent<CardScript>().cardData = cAvaliableCards[i];
            //sets card name in hierachy
            g.name = g.GetComponent<CardScript>().cardData.cardName;
        }
        UpdateDisplay();
        EnemyManagerScript.instance.spawnEnemy();
        initialDrawTurn();
    }

    public void UpdateDisplay() 
    {
        drawCardText.text = drawContainment.childCount.ToString();
        discardCardText.text = discardContainment.childCount.ToString();

        for (int i = 0; i < cardHolder.childCount; i++)
        {
            //check if cards in hand can be played
            //chanes colour of stamina cost.

            CardScript c = cardHolder.transform.GetChild(i).GetComponent<CardScript>();

            if (canUseC(c))
                c.cardStaminaText.color = Color.cyan;
            else c.cardStaminaText.color = Color.red;
        }

    }

    // First card draw eac turn
    public void initialDrawTurn() 
    {
        currentTurn = currentTurnScript.PLAYERTURN;

        endTurnB.interactable = true;
        currentStamina = startStamina;
        combatManagerScript.instance.currentOpps.onNTurn();

        // If starting hand is lager than current hand, draw card.
        if (cardHolder.childCount < startingHand)
        {
            DrawCardText();

        }
        else 
        {
            isDrawStarting = false;
        }

        UIManagerScript.instance.displayUpdate();
        UpdateDisplay();

    }
    public void DrawCardText()
    {
        //number of cards in draw pile 
        if (drawContainment.childCount > 0)
        {
            // draw card(will pick pard between 0 and number of caards that exsist)
            int randomNum = Random.Range(0, drawContainment.childCount);

            drawContainment.GetChild(randomNum).transform.parent = cardHolder;

        }
        else if (drawContainment.childCount <= 0) 
        {
            reshuffleDeck();


        }

        if (isDrawStarting) 
        {
            initialDrawTurn();
        
        }

    }

    public IEnumerator DrawCardss(CardScript card) 
    {
        int amounts = card.cardDraw;

        for (int i = 0; i < amounts; i++)
        {
            DrawCardText();
            Debug.Log("please work mate, draw da card");
            yield return new WaitForSeconds(0.02f); 

        }

        yield return new WaitForEndOfFrame();

        dicardCard(card);

        UpdateDisplay();
    
    }


    public void reshuffleDeck() 
    {
        for (int i = discardContainment.childCount -1; i >= 0; i--)
        {
            Transform temporarayCard = discardContainment.GetChild(i);

            temporarayCard.transform.parent = drawContainment;
            restartCardTransform(temporarayCard);

        }

        UpdateDisplay();
    
    }

    public void dicardCard(CardScript cc) 
    {
        for (int i = 0; i < cardHolder.childCount; i++)
        {
            if (cardHolder.GetChild(i).GetComponent<CardScript>() == cc) 
            {
                Transform temp = cardHolder.GetChild(i);

                temp.transform.parent = discardContainment;
                restartCardTransform(temp);

                UpdateDisplay();

                return;
            
            }

        }
    
    }

    public void restartCardTransform(Transform card) 
    {
        card.localPosition = Vector2.zero;

    
    }

    public bool canUseC(CardScript c) 
    {
        return currentStamina > 0 && c.cardStamina <= currentStamina;
    }

    public void startNewT() 
    {
        combatManagerScript.instance.currentBlock = 0;

        isDrawStarting = true;
        initialDrawTurn();
    
    }
    public void EndTurn() 
    {
        for (int i = cardHolder.childCount -1; i >= 0; i--)
        {
            dicardCard(cardHolder.GetChild(i).GetComponent<CardScript>());

        }

        endTurnB.interactable = false;
        currentTurn = currentTurnScript.OPPTURN;
        combatManagerScript.instance.currentOpps.blockDmg = 0;

        StartCoroutine(EnemyManagerScript.instance.takeETurn(combatManagerScript.instance.currentOpps));
        UpdateDisplay();
        
    
    }
}
