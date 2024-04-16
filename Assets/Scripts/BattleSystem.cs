using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYER3TURN, ENEMY1TURN, ENEMY2TURN, ENEMY3TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject enemy1Prefab;
	public GameObject enemy2Prefab;
	public GameObject enemy3Prefab;

	Unit player1Unit;
	Unit player2Unit;
	Unit player3Unit;

	Unit enemy1Unit;
	Unit enemy2Unit;
	Unit enemy3Unit;

	Unit activePlayer;
	Unit activeEnemy;

	Unit targetedPlayer;
	Unit targetedEnemy;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	bool isActionAllowed;

    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		player1Unit = player1Prefab.GetComponent<Unit>();
		player2Unit = player2Prefab.GetComponent<Unit>();
		player3Unit = player3Prefab.GetComponent<Unit>();

		activePlayer = player1Unit;
		activeEnemy = enemy1Unit;

		enemy1Unit = enemy1Prefab.GetComponent<Unit>();

		// dialogueText.text = enemy1Unit.unitName + " approaches...";

		playerHUD.SetHUD(activePlayer);
		enemyHUD.SetHUD(enemy1Unit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYER1TURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		isActionAllowed = false;
		bool isDead = enemy1Unit.TakeDamage(activePlayer.attack);

		dialogueText.text = activePlayer.name + " used Attack";
		yield return new WaitForSeconds(2f);
		enemyHUD.SetHP(enemy1Unit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} 
		else
		{
			if (state == BattleState.PLAYER1TURN) 
			{
				state = BattleState.PLAYER2TURN;
				activePlayer = player2Unit;
				PlayerTurn();
			}
			else if (state == BattleState.PLAYER2TURN) 
			{
				state = BattleState.PLAYER3TURN;
				activePlayer = player3Unit;
				PlayerTurn();
			}
			else
			{
				state = BattleState.ENEMY1TURN;
				activePlayer = player1Unit;
				playerHUD.SetHUD(activePlayer);
				StartCoroutine(EnemyTurn());
			}
		}
	}

	IEnumerator EnemyTurn()
	{
		int randomTarget = Random.Range(1, 3);

		if (randomTarget == 1)
		{
			targetedPlayer = player1Unit;
		}
		else if (randomTarget == 2)
		{
			targetedPlayer = player2Unit;
		}
		else if (randomTarget == 3)
		{
			targetedPlayer = player3Unit;
		}

		dialogueText.text = activeEnemy.unitName + " used Attack on " + targetedPlayer.unitName;

		yield return new WaitForSeconds(1f);

		bool isDead = activePlayer.TakeDamage(enemy1Unit.attack);

		playerHUD.SetHP(activePlayer.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} 
		else
		{
			state = BattleState.PLAYER1TURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} 
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		isActionAllowed = true;
		playerHUD.SetHUD(activePlayer);
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		state = BattleState.ENEMY1TURN;
		activePlayer.Heal(5);

		playerHUD.SetHP(activePlayer.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (!isActionAllowed)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnDefendButton()
	{
		if (state != BattleState.PLAYER1TURN)
			return;

		StartCoroutine(PlayerHeal());
	}

	public void OnSkill1Button()
	{
		if (activePlayer.unitName == "Alex")
		{
			
		}
	}

	public void OnSkill2Button()
	{
		
	}

}
