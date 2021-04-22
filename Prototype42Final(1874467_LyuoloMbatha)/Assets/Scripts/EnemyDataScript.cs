using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "New Enemy")]
public class EnemyDataScript :ScriptableObject
{
    public EnemyTypesScripts enemyType;
    public string enemyGama;
    public int maxHP;
    public int strangth;
    public EnemyIntentions[] everyIntent;

    [System.Serializable]
    public struct EnemyIntentions 
    {
        public EnemyIntentTypesScript[] intent;
        public int amount;// strangth of enemy intent. 

    }


}
