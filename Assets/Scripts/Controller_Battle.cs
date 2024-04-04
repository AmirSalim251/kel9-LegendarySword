using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYER2TURN, PLAYER3TURN, ENEMYTURN, WON, LOST }

public class Controller_Battle : MonoBehaviour
{
    /*public GameObject ActivePlayer;
    public GameObject ActiveEnemy;*/

    Controller_CharData ActivePlayer;
    Controller_EnemyData ActiveEnemy;

    Controller_CharData ActiveTarget;

    Controller_CharData player1Unit;
    Controller_CharData player2Unit;
    Controller_CharData player3Unit;

    Controller_EnemyData enemyUnit;

    public BattleState state;

    bool isAttackAllowed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(1f);

        player1Unit = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Controller_CharData>();
        player2Unit = GameObject.FindGameObjectWithTag("Player 2").GetComponent<Controller_CharData>();
        player3Unit = GameObject.FindGameObjectWithTag("Player 3").GetComponent<Controller_CharData>();

        ActivePlayer = player1Unit;
        ActiveEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Controller_EnemyData>();

        yield return new WaitForSeconds(1f);

        Debug.Log("Game Start");

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator HeroAttack()
    {
        yield return new WaitForSeconds(2f);

        isAttackAllowed = false;
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
        }
    }

    IEnumerator HeroDefend()
    {
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
        isAttackAllowed = true;
        Debug.Log("Turn: " + ActivePlayer.charName);
    }

    IEnumerator EnemyTurn()
    {

        yield return new WaitForSeconds(1f);

        int RandomTarget = Random.Range(1, 3);

        if (RandomTarget == 1)
        {
            ActiveTarget = player1Unit;
        }
        else if (RandomTarget == 2)
        {
            ActiveTarget = player2Unit;
        }
        else if (RandomTarget == 3)
        {
            ActiveTarget = player3Unit;
        }

        Debug.Log("Enemy targetted: " + ActiveTarget.charName);
        bool isDead = false;
        if (ActiveTarget.isBlocking == false)
        {
            int EnemyDamageOutput = (ActiveEnemy.monsterATK * 4) - (ActiveTarget.charDEF * 2);
            isDead = ActiveTarget.TakeDamage(EnemyDamageOutput);

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

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
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
        /*if (!isAttackAllowed)
            return;*/

        StartCoroutine(HeroAttack());
    }

    public void OnBlockButton()
    {
        StartCoroutine(HeroDefend());
    }
}
