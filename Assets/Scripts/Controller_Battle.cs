using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYER3TURN, ENEMYTURN, WON, LOST }

public class Controller_Battle : MonoBehaviour
{
    public TMP_Text Log;

    public PlayerPanel P1UI;
    public PlayerPanel P2UI;
    public PlayerPanel P3UI;

    int turnCount;
    public TMP_Text turnCounter;

    Controller_CharData ActivePlayer;
    Controller_EnemyData ActiveEnemy;

    Controller_CharData ActiveTarget;

    A_Smash_QTE_Controller qteController;
    Controller_RT rtController;

    public static Controller_CharData player1Unit;
    public static Controller_CharData player2Unit;
    public static Controller_CharData player3Unit;

    public static Controller_EnemyData enemyUnit;

    public BattleState state;

    bool isActionAllowed;
    public bool qteCheck;
    public Animator enemyPanelAnimator;

    void Start()
    {
        qteController = GameObject.FindGameObjectWithTag("QTEController").GetComponent<A_Smash_QTE_Controller>();
        rtController = GameObject.FindGameObjectWithTag("RTController").GetComponent<Controller_RT>();
        qteCheck = false;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        turnCounter.SetText(turnCount.ToString());
    }
    IEnumerator SetupBattle()
    {
        player1Unit = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Controller_CharData>();
        player2Unit = GameObject.FindGameObjectWithTag("Player 2").GetComponent<Controller_CharData>();
        player3Unit = GameObject.FindGameObjectWithTag("Player 3").GetComponent<Controller_CharData>();

        enemyUnit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Controller_EnemyData>();

        ActivePlayer = player1Unit;
        ActiveEnemy = enemyUnit;

        yield return new WaitForSeconds(1f);

        Debug.Log("Game Start");
        Log.text = "Game Start";

        turnCount = 0;
        state = BattleState.ENEMYTURN;
        PassTurn();
    }

    IEnumerator QTEEvent()
    {
        isActionAllowed = false;
        Log.text = "Press 'A' key repeatedly to attack!";
        Debug.Log("Press 'A' key repeatedly to attack!");
        qteController.enabled = true;
        yield return null;
    }

    public IEnumerator RTTime()
    {
        Debug.Log("Its real-time time!");
        rtController.enabled = true;
        yield return null;
    }

    public IEnumerator PlayerAttack()
    {
        isActionAllowed = false;
        Log.text = ActivePlayer.charName + " is attacking!";

        yield return new WaitForSeconds(1f);

        enemyPanelAnimator.SetTrigger("On Hit");
        
        int HeroDamageOutput = (ActivePlayer.charATK * 4) - (ActiveEnemy.monsterDEF * 2);
        bool enemyIsDead = ActiveEnemy.TakeDamage(HeroDamageOutput);

        Debug.Log("Attack berhasil");
        Debug.Log("Damage dihasilkan: " + HeroDamageOutput);
        Debug.Log("Sisa HP " + ActiveEnemy.monsterName + ": " + ActiveEnemy.curHP);

        // Wait before passing turn
        yield return new WaitForSeconds(1.25f);

        if (enemyIsDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            PassTurn();
        }
    }

    IEnumerator PlayerDefend()
    {
        isActionAllowed = false;
        Log.text = ActivePlayer.charName + " is trying to block!";

        yield return new WaitForSeconds(1f);
        
        ActivePlayer.Blocking();
        PassTurn();
        yield return new WaitForSeconds(1f);
    }

    public void HeroBlunder()
    {

    }

    void PlayerTurn()
    {
        isActionAllowed = true;
        Debug.Log("Turn: " + ActivePlayer.charName);
        Log.text = ActivePlayer.charName + "'s turn!";
    }

    public void PassTurn()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            P1UI.MoveDown();

            if (!player2Unit.isDead)
            {
                P2UI.MoveUp();
                state = BattleState.PLAYER2TURN;
                ActivePlayer = player2Unit;
                PlayerTurn();
            }
            else if (!player3Unit.isDead)
            {
                P3UI.MoveUp();
                state = BattleState.PLAYER3TURN;
                ActivePlayer = player3Unit;
                PlayerTurn();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        
        else if (state == BattleState.PLAYER2TURN)
        {
            P2UI.MoveDown();

            if (!player3Unit.isDead)
            {
                P3UI.MoveUp();
                state = BattleState.PLAYER3TURN;
                ActivePlayer = player3Unit;
                PlayerTurn();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        else if (state == BattleState.PLAYER3TURN)
        {
            P3UI.MoveDown();

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
        else if (state == BattleState.ENEMYTURN)
        {
            if (!player1Unit.isDead) 
            {
                P1UI.MoveUp();
                state = BattleState.PLAYER1TURN;
                ActivePlayer = player1Unit;
            }
            else if (!player2Unit.isDead) 
            {
                P2UI.MoveUp();
                state = BattleState.PLAYER2TURN;
                ActivePlayer = player2Unit;
            }
            else if (!player3Unit.isDead) 
            {
                P3UI.MoveUp();
                state = BattleState.PLAYER3TURN;
                ActivePlayer = player3Unit;
            }
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurn()
    {
        Log.text = "Enemy's turn!";

        yield return new WaitForSeconds(1f);

        //Randomizing target for enemy to attack
        while (true)
        {
            int RandomTargetIndex = UnityEngine.Random.Range(0, 3);
            RandomTargetIndex++;
            Debug.Log(RandomTargetIndex);

            if (RandomTargetIndex == 1)
            {
                if (!player1Unit.isDead)
                {
                    ActiveTarget = player1Unit;
                    break;
                }
            }
            else if (RandomTargetIndex == 2)
            {
                if (!player2Unit.isDead)
                {
                    ActiveTarget = player2Unit;
                    break;
                }
            }
            else if (RandomTargetIndex == 3)
            {
                if (!player3Unit.isDead)
                {
                    ActiveTarget = player3Unit;
                    break;
                }
            }
        }
        
        Log.text = "Enemy is attacking " + ActiveTarget.charName;

        if (ActiveTarget.isBlocking == false)
        {
            ActiveEnemy.animator.SetTrigger("isAttack");

            // Wait for the attack animation
            yield return new WaitForSeconds(0.85f);

            int EnemyDamageOutput = (ActiveEnemy.monsterATK * 4) - (ActiveTarget.charDEF * 2);
            ActiveTarget.isDead = ActiveTarget.TakeDamage(EnemyDamageOutput);

            Debug.Log("Attack berhasil");
            Debug.Log("Damage dihasilkan: " + EnemyDamageOutput);
            Debug.Log("Sisa HP " + ActiveTarget.charName + ": " + ActiveTarget.curHP);
        }
        else if (ActiveTarget.isBlocking == true)
        {
            Debug.Log("Attack dihentikan");
            Log.text = ActiveTarget.charName + " blocked enemy's attack!";
        }

        // Wait before passing turn
        yield return new WaitForSeconds(1.25f);

        if (ActiveTarget.isDead)
        {
            Remove(ActiveTarget);

            int PlayerDead = ActiveTarget.charID;
            
            if (player1Unit.isDead == true && player2Unit.isDead == true && player3Unit.isDead == true)
            {
                state = BattleState.LOST;
                EndBattle();
            }
        }
        PassTurn();
        turnCount++;
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            Log.text = "You Won!";
            yield return new WaitForSeconds(1f);
            Debug.Log("You won");
            
        }
        else if (state == BattleState.LOST)
        {
            Log.text = "You Lose!";
            yield return new WaitForSeconds(1f);
            Debug.Log("You lose");
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void OnAttackButton()
    {
        if (!isActionAllowed)
            return;
        if (state == BattleState.PLAYER1TURN)
        {
            StartCoroutine(QTEEvent());
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }        
    }

    public void OnBlockButton()
    {
        if (!isActionAllowed)
            return;
        StartCoroutine(PlayerDefend());
    }

    void Remove(Controller_CharData Character)
    {
        ActiveTarget.gameObject.SetActive(false);
        ActiveTarget.curSP = 0;

        if (Character.charName == "Alex") P1UI.Die();
        else if (Character.charName == "Freya") P2UI.Die();
        else if (Character.charName == "Magnus") P3UI.Die();
        
    }
}
