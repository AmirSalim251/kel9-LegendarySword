using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYER2TURN, PLAYER3TURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit player1Unit;
	Unit player2Unit;
	Unit player3Unit;

	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	bool isAttackAllowed;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		player1Unit = playerPrefab.GetComponent<Unit>();
		player2Unit = player2Prefab.GetComponent<Unit>();
		player3Unit = player3Prefab.GetComponent<Unit>();
		playerUnit = player1Unit;

		enemyUnit = enemyPrefab.GetComponent<Unit>();

		dialogueText.text = enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		isAttackAllowed = false;
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		dialogueText.text = playerUnit.name + " chose Attack";
		yield return new WaitForSeconds(2f);
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} 
		else
		{
			if (state == BattleState.PLAYERTURN) 
			{
				state = BattleState.PLAYER2TURN;
				playerUnit = player2Unit;
				PlayerTurn();
			}
			else if (state == BattleState.PLAYER2TURN) 
			{
				state = BattleState.PLAYER3TURN;
				playerUnit = player3Unit;
				PlayerTurn();
			}
			else
			{
				state = BattleState.ENEMYTURN;
				playerUnit = player1Unit;
				playerHUD.SetHUD(playerUnit);
				StartCoroutine(EnemyTurn());
			}
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		isAttackAllowed = true;
		playerHUD.SetHUD(playerUnit);
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		state = BattleState.ENEMYTURN;
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (!isAttackAllowed)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

}
