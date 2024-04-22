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

    public TMP_Text P1Name;
    public Image Player1HealthBar;
    public float Player1HealthAmount;
    public float Player1HealthMax;
    public float Player1SPAmount;
    public float Player1SPMax;

    public TMP_Text P2Name;
    public Image Player2HealthBar;
    public float Player2HealthAmount;
    public float Player2HealthMax;
    public float Player2SPAmount;
    public float Player2SPMax;

    public TMP_Text P3Name;
    public Image Player3HealthBar;
    public float Player3HealthAmount;
    public float Player3HealthMax;
    public float Player3SPAmount;
    public float Player3SPMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1Name.text = Controller_Battle.player1Unit.charName;
        P2Name.text = Controller_Battle.player2Unit.charName;
        P3Name.text = Controller_Battle.player3Unit.charName;
        EnemyName.text = Controller_Battle.enemyUnit.monsterName;

        GetEnemyHealth();
        GetPlayerHealth();
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

        Player1HealthBar.fillAmount = Player1HealthAmount/ Player1HealthMax;
        Player2HealthBar.fillAmount = Player2HealthAmount / Player2HealthMax;
        Player3HealthBar.fillAmount = Player3HealthAmount / Player3HealthMax;
    }

    void GetPlayerSP()
    {
        Player1SPMax = Controller_Battle.player1Unit.baseSP;
        Player2SPMax = Controller_Battle.player2Unit.baseSP;
        Player3SPMax = Controller_Battle.player3Unit.baseSP;


    }
}
