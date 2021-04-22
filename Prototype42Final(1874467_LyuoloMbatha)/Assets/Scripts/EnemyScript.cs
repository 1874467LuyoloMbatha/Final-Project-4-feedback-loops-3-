using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyScript : MonoBehaviour
{
    public EnemyDataScript Edata;

    [Header("Enemy Information")]
    public string eName;
    public EnemyTypesScripts Etype;
    public int maxxHP;
    public int strength;
    public int blockDmg;

    public Slider healthSlider;
    public Image healthSliderfiller;
    public Color blockColour;

    public GameObject cBlockedDisplay;

    public Text healthText;
    public Text enemyN;

    [Header("Enemy Intent Display")]
    public Image intentSthombe;
    public Text intentAmountT;
    public Text blockedAmointDis;

    public Sprite s_IntentAttack;
    public Sprite s_IntentDefence;
    public Sprite s_IntentBuff;
    public Sprite s_IntentDismantle;

    public List<EnemyDataScript.EnemyIntentions> thisTurnInt = new List<EnemyDataScript.EnemyIntentions>();
    public int thisTurnIntStrength;

    [SerializeField]
    private int currentHP;
    private int CurrentHp { get { return currentHP; } set { currentHP = value; HandleHealtHH(); } }

    private void Start()
    {
        collectInfoData();
    }
    private void collectInfoData()
    {
        if (Edata == null)
        {
            Destroy(this.gameObject);
            return;
        }

        eName = Edata.enemyGama;
        Etype = Edata.enemyType;
        maxxHP = Edata.maxHP;
        strength = Edata.strangth;

        healthSlider.maxValue = maxxHP;
        enemyN.text = eName.ToUpper();
        CurrentHp = maxxHP;
    }

    private void HandleHealtHH()
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
            //gameover/end matc screen
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);

        }

        if (currentHP > maxxHP)
            currentHP = maxxHP;

        healthSlider.value = currentHP;
        healthText.text = string.Format("{0}/{1}", currentHP, maxxHP);

        if (blockDmg <= 0)
        {
            blockDmg = 0;
            healthSliderfiller.color = Color.green;
            cBlockedDisplay.SetActive(false);
        }
    }

    public void TakeDmg(int d)
    {
        int dmg = d;

        //Status effects 

        if (blockDmg >= dmg) //enemy block is > tHan player damage
            blockDmg -= dmg;
        else
        {
            dmg -= blockDmg;

            CurrentHp -= dmg;
        }

        blockedAmointDis.text = blockDmg.ToString();
    }

    public void HealsHealtH(int d) //fasilitates Healin cast for enemies 
    {
        CurrentHp += d;
    }

    public void addDefensive(int d) 
    {
        blockDmg += d;

        if (blockDmg > 0) 
        {
            cBlockedDisplay.SetActive(true);
            healthSliderfiller.color = blockColour;
            blockedAmointDis.text = blockDmg.ToString();
        }

        HandleHealtHH();
    }

    // after player drwas card 
    public void onNTurn() 
    {
        thisTurnInt.Clear();

        //enemymanager picks next instance
        EnemyManagerScript.instance.cooseIntentFNT(this);

    }
}

