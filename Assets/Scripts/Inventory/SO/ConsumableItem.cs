using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Items/Consumable", order = 2)]
public class ConsumableItem : BaseItem
{
    public Controller_Battle battleData;
    public Controller_CharData charData;

    /*[SerializeField]
    private List<ModifierData> modifiersData = new List<ModifierData>();*/

    public int effectHP;
    public int effectSP;
    public int effectATK;
    public int effectDEF;

    public ConsumableType consumableType;
    public bool isItemUsed;

    public override ItemType GetItemType()
    {
        return ItemType.Consumable;
    }

    public ConsumableType GetConsumableType()
    {
        return consumableType;
    }

    public override bool UseItem(Controller_CharData charTarget)
    {
        /*UsingItem(charTarget);
        battleData = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
*/
        /*Debug.Log("Used Consumable");*/

        battleData = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();

        switch (consumableType)
        {
            case ConsumableType.Restorative:
                if (charTarget.curHP == charTarget.baseHP)
                {
                    isItemUsed = false;
                    Debug.Log("HP " + charTarget.charName + " penuh!");
                }
                else if (charTarget.curHP != charTarget.baseHP)
                {
                    if (effectHP > 0)
                        charTarget.Heal(effectHP);

                    isItemUsed = true;
                }

                if (charTarget.curSP == charTarget.baseSP)
                {
                    isItemUsed = false;
                    Debug.Log("SP " + charTarget.charName + " penuh!");
                }
                else if(charTarget.curSP != charTarget.baseSP)
                {
                    if (effectSP > 0)
                        charTarget.RestoreMP(effectSP);

                    charTarget.RestoreMP(effectSP);
                    isItemUsed = true;
                }

                break;

            case ConsumableType.Revive:
                if(!charTarget.gameObject.activeSelf)
                {
                    charTarget.gameObject.SetActive(true);
                    charTarget.isDead = false;
                    charTarget.Heal(effectHP);

                    battleData.PlayerAlive = battleData.InsertPlayerAlive(battleData.PlayerAlive, charTarget.charID);

                    isItemUsed = true;

                    battleData.InsertPlayerAlive(battleData.PlayerAlive, charTarget.charID);

                }
                else
                {
                    Debug.Log("Char masih hidup!");
                    isItemUsed = false;
                }

                break;

        }
        return isItemUsed;
    }

    public bool GetItemStatus()
    {
        return isItemUsed;
    }

   /* public Boolean UsingItem(Controller_CharData charTarget)
    {
        switch (consumableType)
        {
            case ConsumableType.Restorative:
                if (charTarget.curHP == charTarget.baseHP)
                {
                    isItemUsed = false;
                }

                if (charTarget.curHP != charTarget.baseHP)
                {
                    charTarget.curHP += effectHP;
                    isItemUsed = true;
                }

                if (charTarget.curSP != charTarget.baseSP)
                {
                    charTarget.curSP += effectSP;
                    isItemUsed = true;
                }
                else
                {
                    isItemUsed = false;
                }
                break;

            case ConsumableType.Revive:
                charTarget.gameObject.SetActive(true);
                charTarget.isDead = false;
                charTarget.curHP += effectHP;
                break;

        }

        return isItemUsed;
    }*/

    
}

/*[Serializable]
public class ModifierData
{
    public CharacterStatsModifier statsModifier;
    public int value;
}*/

public enum ConsumableType
{
    Restorative,
    Revive
}
