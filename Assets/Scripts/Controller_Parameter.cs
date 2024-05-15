using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Parameter : MonoBehaviour
{
    public TMP_Text EnemyName;
    public Image EnemyHealthBar;
    public float EnemyHealthAmount;
    public float EnemyHealthMax;

    [Space]
    public TMP_Text P1Name;
    public TMP_Text P1TargetItem;
    public Image Player1HealthBar;
    public float Player1HealthAmount;
    public float Player1HealthMax;
    public Image Player1SPBar;
    public float Player1SPAmount;
    public float Player1SPMax;

    [Space]
    public TMP_Text P2Name;
    public TMP_Text P2TargetItem;
    public Image Player2HealthBar;
    public float Player2HealthAmount;
    public float Player2HealthMax;
    public Image Player2SPBar;
    public float Player2SPAmount;
    public float Player2SPMax;

    [Space]
    public TMP_Text P3Name;
    public TMP_Text P3TargetItem;
    public Image Player3HealthBar;
    public float Player3HealthAmount;
    public float Player3HealthMax;
    public Image Player3SPBar;
    public float Player3SPAmount;
    public float Player3SPMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerName();
        GetPlayerHealth();
        GetPlayerSP();

        GetEnemyName();
        GetEnemyHealth();
    }



    void GetEnemyName()
    {
        EnemyName.text = Controller_Battle.enemyUnit.monsterName;
    }

    void GetEnemyHealth()
    {
        EnemyHealthMax = Controller_Battle.enemyUnit.baseHP;
        EnemyHealthAmount = Controller_Battle.enemyUnit.curHP;
        EnemyHealthBar.fillAmount = EnemyHealthAmount / EnemyHealthMax;
    }

    void GetPlayerHealth()
    {
        Player1HealthMax = Controller_Battle.player1Unit.baseHP;
        Player2HealthMax = Controller_Battle.player2Unit.baseHP;
        Player3HealthMax = Controller_Battle.player3Unit.baseHP;

        Player1HealthAmount = Controller_Battle.player1Unit.curHP;
        Player2HealthAmount = Controller_Battle.player2Unit.curHP;
        Player3HealthAmount = Controller_Battle.player3Unit.curHP;

        Player1HealthBar.fillAmount = Player1HealthAmount / Player1HealthMax;
        Player2HealthBar.fillAmount = Player2HealthAmount / Player2HealthMax;
        Player3HealthBar.fillAmount = Player3HealthAmount / Player3HealthMax;
    }

    void GetPlayerSP()
    {
        Player1SPMax = Controller_Battle.player1Unit.baseSP;
        Player2SPMax = Controller_Battle.player2Unit.baseSP;
        Player3SPMax = Controller_Battle.player3Unit.baseSP;

        Player1SPAmount = Controller_Battle.player1Unit.curSP;
        Player2SPAmount = Controller_Battle.player2Unit.curSP;
        Player3SPAmount = Controller_Battle.player3Unit.curSP;

        Player1SPBar.fillAmount = Player1SPMax / Player1SPAmount;
        Player2SPBar.fillAmount = Player2SPMax / Player2SPAmount;
        Player3SPBar.fillAmount = Player3SPMax / Player3SPAmount;
    }

    void GetPlayerName()
    {
        P1Name.text = Controller_Battle.player1Unit.charName;
        P2Name.text = Controller_Battle.player2Unit.charName;
        P3Name.text = Controller_Battle.player3Unit.charName;

        P1TargetItem.text = Controller_Battle.player1Unit.charName;
        P2TargetItem.text = Controller_Battle.player2Unit.charName;
        P3TargetItem.text = Controller_Battle.player3Unit.charName;
    }
}
