using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYER2TURN, PLAYER3TURN, ENEMYTURN, WON, LOST }

public class Controller_Battle : MonoBehaviour
{
    /*public GameObject ActivePlayer;
    public GameObject ActiveEnemy;*/

    int turnCount;
    public TMP_Text turnCounter;

    Controller_CharData ActivePlayer;
    Controller_EnemyData ActiveEnemy;

    Controller_CharData ActiveTarget;

    public static Controller_CharData player1Unit;
    public static Controller_CharData player2Unit;
    public static Controller_CharData player3Unit;

    public int[] PlayerAlive = {1,2,3,4};

    Controller_EnemyData enemyUnit;

    public BattleState state;

    bool isActionAllowed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        turnCounter.SetText(turnCount.ToString());
    }
    IEnumerator SetupBattle()
    {
        player1Unit = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Controller_CharData>();
        player2Unit = GameObject.FindGameObjectWithTag("Player 2").GetComponent<Controller_CharData>();
        player3Unit = GameObject.FindGameObjectWithTag("Player 3").GetComponent<Controller_CharData>();

        yield return new WaitForSeconds(1f);
        
        ActivePlayer = player1Unit;
        ActiveEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Controller_EnemyData>();

        yield return new WaitForSeconds(1f);

        Debug.Log("Game Start");

        turnCount = 0;
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator HeroAttack()
    {
        isActionAllowed = false;

        yield return new WaitForSeconds(2f);
        
        int HeroDamageOutput = 0;
        HeroDamageOutput = (ActivePlayer.charATK * 4) - (ActiveEnemy.monsterDEF * 2);
        /*ActiveEnemy.GetComponent<Controller_EnemyData>().curHP -= HeroDamageOutput;*/

        bool isDead = ActiveEnemy.TakeDamage(HeroDamageOutput);

        if (ActiveEnemy.baseHP != ActiveEnemy.curHP)
        {
            Debug.Log("Attack berhasil");
            Debug.Log("Damage dihasilkan: " + HeroDamageOutput);
            Debug.Log("Sisa HP " + ActiveEnemy.monsterName + ": " + ActiveEnemy.curHP);
        }
        else
        {
            Debug.Log("Attack gagal");
        }

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if (state == BattleState.PLAYERTURN)
            {
                if (!player2Unit.isDead)
                {
                    state = BattleState.PLAYER2TURN;
                    ActivePlayer = player2Unit;
                    PlayerTurn();
                }
                else
                {
                    state = BattleState.PLAYER3TURN;
                    ActivePlayer = player3Unit;
                    PlayerTurn();
                }
                
            }
            else if (state == BattleState.PLAYER2TURN)
            {
                if (!player3Unit.isDead)
                {
                    state = BattleState.PLAYER3TURN;
                    ActivePlayer = player3Unit;
                    PlayerTurn();
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    ActivePlayer = player1Unit;
                    StartCoroutine(EnemyTurn());
                }
            }
            else
            {
                state = BattleState.ENEMYTURN;
                if (player1Unit.isDead)
                {
                    ActivePlayer = player2Unit;
                    if (player2Unit.isDead)
                    {
                        ActivePlayer = player3Unit;
                    }
                }
                else
                {
                    ActivePlayer = player1Unit;
                }
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator HeroDefend()
    {
        isActionAllowed = false;

        yield return new WaitForSeconds(1f);

        ActivePlayer.Blocking();

        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.PLAYER2TURN;
            ActivePlayer = player2Unit;
            PlayerTurn();
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            state = BattleState.PLAYER3TURN;
            ActivePlayer = player3Unit;
            PlayerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            ActivePlayer = player1Unit;
            StartCoroutine(EnemyTurn());
        }

        yield return new WaitForSeconds(1f);

    }

    public void HeroBlunder()
    {

    }

    void PlayerTurn()
    {
        isActionAllowed = true;
        turnCount++;
        Debug.Log("Turn: " + ActivePlayer.charName);
    }

    IEnumerator EnemyTurn()
    {
        isActionAllowed = false;

        yield return new WaitForSeconds(1f);

        /*int RandomTarget = UnityEngine.Random.Range(1, 4);*/
        /*int RandomTarget = UnityEngine.Random.Range(PlayerAlive.Min(), PlayerAlive.Max());

        if (RandomTarget == 1)
        {
            if(player1Unit.isDead)
            {
                RandomTarget = UnityEngine.Random.Range(2, 4);
                if (RandomTarget == 2)
                {
                    ActiveTarget = player2Unit;
                }
                else if (RandomTarget == 3)
                {
                    ActiveTarget = player3Unit;
                }
            }
            ActiveTarget = player1Unit;
        }
        else if (RandomTarget == 2)
        {
            ActiveTarget = player2Unit;
        }
        else if (RandomTarget == 3)
        {
            ActiveTarget = player3Unit;
        }*/

        int RandomTargetIndex = UnityEngine.Random.Range(0, PlayerAlive.Length-1);
        int RandomTargetVal = PlayerAlive[RandomTargetIndex];

        Debug.Log(RandomTargetVal);

        if (RandomTargetVal == 1)
        {
            ActiveTarget = player1Unit;
        }
        else if (RandomTargetVal == 2)
        {
            ActiveTarget = player2Unit;
        }
        else if (RandomTargetVal == 3)
        {
            ActiveTarget = player3Unit;
        }


        Debug.Log("Enemy targetted: " + ActiveTarget.charName);

        if (ActiveTarget.isBlocking == false)
        {
            int EnemyDamageOutput = (ActiveEnemy.monsterATK * 4) - (ActiveTarget.charDEF * 2);
            ActiveTarget.isDead = ActiveTarget.TakeDamage(EnemyDamageOutput);

            Debug.Log("Attack berhasil");
            Debug.Log("Damage dihasilkan: " + EnemyDamageOutput);
            Debug.Log("Sisa HP " + ActiveTarget.charName + ": " + ActiveTarget.curHP);
        }
        else if (ActiveTarget.isBlocking == true)
        {
            Debug.Log("Attack dihentikan");
        }
        
        /*playerHUD.SetHP(playerUnit.currentHP);*/

        yield return new WaitForSeconds(1f);

        if (ActiveTarget.isDead)
        {
            ActiveTarget.gameObject.SetActive(false);
            int PlayerDead = ActiveTarget.charID;
            int[] UpdatePlayerAlive = PlayerAlive.Where(val => val != PlayerDead).ToArray();
            PlayerAlive = UpdatePlayerAlive;
            
            if (player1Unit.isDead == true && player2Unit.isDead == true && player3Unit.isDead == true)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                if (player1Unit.isDead)
            {
                ActivePlayer = player2Unit;
                state = BattleState.PLAYER2TURN;
                if (player2Unit.isDead)
                {
                    ActivePlayer = player3Unit;
                    state = BattleState.PLAYER3TURN;
                }
            }
            else
            {
                ActivePlayer = player1Unit;
                state = BattleState.PLAYERTURN;

            }
                PlayerTurn();
            }
            
        }
        else
        {
            if (player1Unit.isDead)
            {
                ActivePlayer = player2Unit;
                state = BattleState.PLAYER2TURN;
                if (player2Unit.isDead)
                {
                    ActivePlayer = player3Unit;
                    state = BattleState.PLAYER3TURN;
                }
            }
            else
            {
                ActivePlayer = player1Unit;
                state = BattleState.PLAYERTURN;

            }
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            /*dialogueText.text = "You won the battle!";*/
            Debug.Log("You won");
        }
        else if (state == BattleState.LOST)
        {
            /*dialogueText.text = "You were defeated.";*/
            Debug.Log("You lose");
        }
    }

    public void OnAttackButton()
    {
        if (!isActionAllowed)
            return;

        StartCoroutine(HeroAttack());
    }

    public void OnBlockButton()
    {
        if (!isActionAllowed)
            return;
        StartCoroutine(HeroDefend());
    }

}
