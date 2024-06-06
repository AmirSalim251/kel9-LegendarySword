using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYER3TURN, ENEMYTURN, WON, LOST }

public class Controller_Battle : MonoBehaviour
{
    public GameController gameController;

    public GameObject combatUI;

    public TMP_Text Log;

    public PlayerPanel P1UI;
    public PlayerPanel P2UI;
    public PlayerPanel P3UI;

    

    Controller_CharData ActivePlayer;
    Controller_EnemyData ActiveEnemy;

    Controller_CharData ActiveTarget;

    A_Smash_QTE_Controller qteController;
    Controller_RT rtController;
    

    public static Controller_CharData player1Unit;
    public static Controller_CharData player2Unit;
    public static Controller_CharData player3Unit;

    public int[] PlayerAlive = {1,2,3,4};

    public static Controller_EnemyData enemyUnit;

    public BattleState state;

    bool isActionAllowed;
    public bool qteCheck;
    public Animator enemyPanelAnimator;

    [Header("Objective")]
    public int turnCount;
    public TMP_Text turnCounter;

    public int totalHP;
    public float totalHPDelta;

    public int totalCharDead;

    /*void Awake()
    {
        combatUI = GameObject.FindGameObjectWithTag("CombatUI");
    }*/

    // Start is called before the first frame update
    void Start()
    {
        qteController = GameObject.FindGameObjectWithTag("QTEController").GetComponent<A_Smash_QTE_Controller>();
        rtController = GameObject.FindGameObjectWithTag("RTController").GetComponent<Controller_RT>();
        qteCheck = false;
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

        /*yield return new WaitForSeconds(1f);*/

        enemyUnit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Controller_EnemyData>();

        ActivePlayer = player1Unit;
        /*ActiveEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Controller_EnemyData>();*/
        ActiveEnemy = enemyUnit;

        yield return new WaitForSeconds(1f);

        Debug.Log("Game Start");
        Log.text = "Game Start";

        turnCount = 0;
        state = BattleState.PLAYER1TURN;
        PlayerTurn();
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

        AudioManager.Instance.PlaySFX("playerHit");

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

        isActionAllowed = false;

        yield return new WaitForSeconds(1f);

        //Randomizing target for enemy to attack
        while (true)
        {
            int RandomTargetIndex = UnityEngine.Random.Range(0, PlayerAlive.Length);
            int RandomTargetVal = PlayerAlive[RandomTargetIndex];
            Debug.Log(RandomTargetVal);

            if (RandomTargetVal == 1)
            {
                if (!player1Unit.isDead)
                {
                    ActiveTarget = player1Unit;
                    break;
                }
            }
            else if (RandomTargetVal == 2)
            {
                if (!player2Unit.isDead)
                {
                    ActiveTarget = player2Unit;
                    break;
                }
            }
            else if (RandomTargetVal == 3)
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

            AudioManager.Instance.PlaySFX("enemyHit");

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

            //reset target block state to false
            ActiveTarget.isBlocking = false;
        }

        // Wait before passing turn
        yield return new WaitForSeconds(1.25f);

        if (ActiveTarget.isDead)
        {
            Remove(ActiveTarget);

            if (player1Unit.isDead == true && player2Unit.isDead == true && player3Unit.isDead == true)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
            
        }
        PassTurn();
        turnCount++;
    }
    IEnumerator EndBattle()
    {
        AudioManager.Instance.bgmSource.Stop();
        if (state == BattleState.WON)
        {
            //scoring check
            CalculateHPDelta();
            CheckPlayerDead();

            Log.text = "You Won!";
            gameController.transitionPanel.SetActive(true);
            gameController.transitionPanel.GetComponent<Animator>().Play("Scroll Left");
            yield return new WaitForSeconds(1f);
            Debug.Log("You won");

            AudioManager.Instance.PlayMusic(AudioManager.Instance.winSong);

            gameController.scoreController.CheckScore(true);

            //switch from combatUI to resultPanel
            HideCombatUI();
            
            AudioManager.Instance.PlayMusic(AudioManager.Instance.winSong);
            gameController.transitionPanel.GetComponent<Animator>().Play("Scroll Right");
            gameController.transitionPanel.SetActive(false);

            gameController.endPanel.SetActive(true);

        }
        else if (state == BattleState.LOST)
        {
            //scoring check
            CalculateHPDelta();
            CheckPlayerDead();

            Log.text = "You Lose!";
            gameController.transitionPanel.SetActive(true);
            gameController.transitionPanel.GetComponent<Animator>().Play("Scroll Left");
            yield return new WaitForSeconds(2f);
            Debug.Log("You lose");

            AudioManager.Instance.PlayMusic(AudioManager.Instance.winSong);

            gameController.scoreController.CheckScore(false);

            //switch from combatUI to resultPanel
            HideCombatUI();
             
            gameController.transitionPanel.GetComponent<Animator>().Play("Scroll Right");
            

            yield return new WaitForSeconds(1f);

            gameController.transitionPanel.SetActive(false);
            gameController.endPanel.SetActive(true);
        }
    }

    public void OnAttackButton()
    {
        if (!isActionAllowed)
            return;
        if (state == BattleState.PLAYER1TURN)
        {
            StartCoroutine(QTEEvent());
            // qteController.enabled = false;
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

    
    public void CalculateHPDelta()
    {
        totalHP = player1Unit.baseHP + player2Unit.baseHP + player3Unit.baseHP;
        int totalHPMinus = player1Unit.curHP + player2Unit.curHP + player3Unit.curHP;
        Debug.Log("total HP" + totalHP);
        Debug.Log("HP minus" + totalHPMinus);

        totalHPDelta = ((float)(totalHP - totalHPMinus) / totalHP) * 100;
        totalHPDelta = (float)Math.Floor(totalHPDelta);

        Debug.Log(totalHPDelta);
        /*totalHPDelta = totalHPDelta / totalHP * 100;*/
    }

    public void HideCombatUI()
    {
        CanvasGroup combatUIGroup = combatUI.GetComponent<CanvasGroup>();

        combatUIGroup.alpha = 0f; // Make UI element invisible
        combatUIGroup.interactable = false; // Disable interaction
        combatUIGroup.blocksRaycasts = false; // Disable raycasting
    }

    public void ShowCombatUI()
    {
        CanvasGroup combatUIGroup = combatUI.GetComponent<CanvasGroup>();

        combatUIGroup.alpha = 1.0f; // Make UI element visible
        combatUIGroup.interactable = true; // Enable interaction
        combatUIGroup.blocksRaycasts = true; // Enable raycasting
    }



    public void CheckPlayerDead()
    {
        if (player1Unit.isDead == true)
        {
            totalCharDead++;
        }
        
        if (player2Unit.isDead == true)
        {
            totalCharDead++;
        }

        if (player3Unit.isDead == true)
        {
            totalCharDead++;
        }
    }

    public int[] InsertPlayerAlive(int[] originalArray, int newElement)
    {
        int x = (newElement - 1);
        int[] newArray = new int[originalArray.Length + 1];

        // Copy elements up to the newElement's position
        for (int i = 0; i < x; i++)
        {
            newArray[i] = originalArray[i];
        }

        // Insert the new element
        newArray[x] = newElement;

        // Copy the remaining elements
        for (int i = x; i < originalArray.Length; i++)
        {
            newArray[i + 1] = originalArray[i];
        }

        return newArray;
    }

    void Remove(Controller_CharData Character)
    {
        ActiveTarget.curSP = 0;
        ActiveTarget.gameObject.SetActive(false);

        //updating playerAlive
        int PlayerDead = ActiveTarget.charID;
        int[] UpdatePlayerAlive = PlayerAlive.Where(val => val != PlayerDead).ToArray();
        PlayerAlive = UpdatePlayerAlive;

        if (Character.charName == "Alex") P1UI.Die();
        else if (Character.charName == "Freya") P2UI.Die();
        else if (Character.charName == "Magnus") P3UI.Die();
    }
}
