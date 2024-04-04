using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_EnemyData : MonoBehaviour
{
    public string monsterName;
    public int monsterID;

    public float baseHP;

    public float curHP;

    public int monsterATK;
    public int monsterDEF;

    // Start is called before the first frame update
    void Start()
    {
        curHP = baseHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(int damage)
    {
        curHP -= damage;

        if (curHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
