using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{
    public static EnemyManagerScript instance;

    public Transform spawnS;

    public GameObject enemyPre;

    [Header("All ENEMY DATA LA")]
    public List<int> enemyIn = new List<int>();
    public List<EnemyDataScript> enemyINData = new List<EnemyDataScript>();
    public Dictionary<int, EnemyDataScript> enemyDictionary = new Dictionary<int, EnemyDataScript>(); 

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;


        for (int i = 0; i < enemyINData.Count; i++)
        {
            enemyIn.Add(i);
        }

        for (int i = 0; i < enemyIn.Count; i++)
        {
            enemyDictionary.Add(i, enemyINData[i]);
        }
        //refers to all the scrptable enemy objects.

    }


    public void spawnEnemy()
    {
        // will pick a random enemy type
        int r = Random.Range(0, enemyDictionary.Count);
        GameObject f = Instantiate(enemyPre, spawnS);
        EnemyScript e = f.GetComponent<EnemyScript>();

        e.Edata = enemyDictionary[r];
        combatManagerScript.instance.currentOpps = e;
    }

    public void cooseIntentFNT(EnemyScript e) 
    {
        int r = Random.Range(0, e.Edata.everyIntent.Length);

        e.thisTurnInt.Clear();

        EnemyDataScript.EnemyIntentions intent = e.Edata.everyIntent[r];

        for (int i = 0; i < intent.intent.Length; i++)
        {
            e.thisTurnInt.Add(intent);
        }

        e.thisTurnIntStrength = intent.amount;
      

        switch (e.thisTurnInt[0].intent[0])
        {
            case EnemyIntentTypesScript.ATTACK:
                int dmg = e.thisTurnIntStrength;

                e.intentSthombe.sprite = e.s_IntentAttack;
                e.intentAmountT.text = dmg.ToString();
                break;
            case EnemyIntentTypesScript.DEFEND:
                e.intentSthombe.sprite = e.s_IntentDefence;
                e.intentAmountT.text = "";
                break;
            case EnemyIntentTypesScript.BUFF:
                e.intentSthombe.sprite = e.s_IntentBuff;
                e.intentAmountT.text = "";
                break;
            case EnemyIntentTypesScript.DISMANTLE:
                e.intentSthombe.sprite = e.s_IntentDismantle;
                e.intentAmountT.text = "";
                break;
        }
    }

    public IEnumerator takeETurn(EnemyScript e) 
    {
        yield return new WaitForSeconds(0.5f);
        //enemy performs action/intention and vfx effect.
        for (int i = 0; i < e.thisTurnInt.Count; i++)
        {
            switch (e.thisTurnInt[i].intent[i])
            {
                case EnemyIntentTypesScript.ATTACK:
                    combatManagerScript.instance.TakeDmg(e.thisTurnIntStrength);
                    break;
                case EnemyIntentTypesScript.DEFEND:
                    e.addDefensive(e.thisTurnIntStrength);
                    break;
                case EnemyIntentTypesScript.BUFF:
                    break;
                case EnemyIntentTypesScript.DISMANTLE:
                    break;
            }
        }
        yield return new WaitForSeconds(1.5f);
        endOppTurn();
    }

    public void endOppTurn() 
    {
        //TO DO: Reduce player sp effects

        cardmanagerScript.instance.startNewT();
    
    }


}
