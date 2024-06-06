using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Controller_CharData : MonoBehaviour
{
    public static Model_CharData charData1 = new Model_CharData();
    public Model_CharData charData2 = new Model_CharData();
    public Model_CharData charData3 = new Model_CharData();

    public string charName;
    public int charID;

    /*public int charLevel;
    public int charEXP;*/

    public int baseHP;
    public int baseSP;

    public int curHP;
    public int curSP;

    public int charATK;
    public int charDEF;

    public bool isDead = false;
    public bool isBlocking = false;
    int DefChance;

    public Animator panelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        /* if(charID == 1)
         {
             charData1 = GameController.charDataAlex;
         }
         else if(charID == 2)
         {

         }
         else if(charID == 3)
         {

         }*/

        /*GetCharData();*/

        curHP = baseHP;
        curSP = baseSP;

        SetCharData(charData1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCharData()
    {
        charName = charData1.charName;
        charID = charData1.charID;
        /*charLevel = charData1.charLevel;
        charEXP = charData1.charEXP;*/
        baseHP = charData1.baseHP;
        baseSP = charData1.baseSP;
        charATK = charData1.charATK;
        charDEF = charData1.charDEF;
        /*STR = charData1.STR;
        DEX = charData1.DEX;
        INT = charData1.INT;*/

        curHP = baseHP;
        curSP = baseSP;
    }

    public void SetCharData(Model_CharData charData)
    {
        charData.charName = charName;
        charData.charID = charID;
        /*charData.charLevel = charLevel;
        charData.charEXP = charEXP;*/
        charData.baseHP = baseHP;
        charData.baseSP = baseSP;

        charData.charATK = charATK;
        charData.charDEF = charDEF;
        /*charData.STR = STR;
        charData.DEX = DEX;
        charData.INT = INT;*/
    }

    public bool TakeDamage(int damage)
    {
        VFX.instance.Create(transform.position, damage.ToString(), charName);
        panelAnimator.SetTrigger("OnHit");

        curHP -= damage;

        if(curHP <= 0)
        {
            curHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        curHP += amount;
        if (curHP > baseHP)
            curHP = baseHP;
    }

    public void UseMP(int amount)
    {
        curSP -= amount;
        if (curSP < 0)
            curSP = 0;
    }

    public void RestoreMP(int amount)
    {
        curSP += amount;
        if (curSP > baseSP) 
            curSP = baseSP;
    }

    public void Blocking()
    {
        DefChance = Random.Range(0,2);
        if (DefChance == 1)
        {
            Debug.Log("Block berhasil");
            isBlocking = true;
        }
        else if(DefChance == 0)
        {
            Debug.Log("Block gagal");
            isBlocking = false;
        }
    }
    
}
