using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Card_", menuName = "New Card")]
public class cardDealScript : ScriptableObject
{
    public string cardName;
    public CardTypesScript type;
    public int cardStamina; //card cost, similar to mana.

    [Multiline] //creats a bigger text box( more space to string feild).
    public string cardDescript; // Description of card.

    public int strength; // card damage.
    public int defensive; // card blocking ability.
    public int drawCardAmmount;
}
