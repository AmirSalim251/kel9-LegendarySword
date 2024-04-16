using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class UnitStats
{
	public string unitName;
    
    public int maxHP;
    public int maxMP;
    public int attack;
    public int defense;
	public int currentHP;
	public int currentMP;

	public UnitStats (string unitName) 
    {
        if (unitName == "Alex")
        {
            maxHP = 150;
            maxMP = 5;
            attack = 25;
            defense = 20;
        }
        else if (unitName == "Freya")
        {
            maxHP = 120;
            maxMP = 6;
            attack = 18;
            defense = 15;
        }
        else if (unitName == "Magnus")
        {
            maxHP = 130;
            maxMP = 6;
            attack = 15;
            defense = 25;
        }
		else if (unitName == "Mob Minion")
		{
			maxHP = 70;
            attack = 15;
            defense = 15;
		}
		else if (unitName == "Mob Boss")
		{
			maxHP = 350;
            attack = 25;
            defense = 30;
		}
        
        currentHP = maxHP;
        currentMP = maxMP;
    }

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}
