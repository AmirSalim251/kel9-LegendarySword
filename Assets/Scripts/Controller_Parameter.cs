using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Parameter : MonoBehaviour
{
    private Controller_Battle battleController;

    [Header("Enemy")]
    public TMP_Text EnemyName;
    public Image EnemyHealthBar;
    public float EnemyHealthAmount;
    public float EnemyHealthMax;

    [Header("P1")]
    public TMP_Text P1Name;
    public TMP_Text P1TargetItem;
    public Image Player1HealthBar;
    public float Player1HealthAmount;
    public TMP_Text Player1HealthAmountText;
    public float Player1HealthMax;
    public Image Player1SPBar;
    public float Player1SPAmount;
    public TMP_Text Player1SPAmountText;
    public float Player1SPMax;
    private GameObject P1UIGroup;

    [Header("P2")]
    public TMP_Text P2Name;
    public TMP_Text P2TargetItem;
    public Image Player2HealthBar;
    public float Player2HealthAmount;
    public TMP_Text Player2HealthAmountText;
    public float Player2HealthMax;
    public Image Player2SPBar;
    public float Player2SPAmount;
    public TMP_Text Player2SPAmountText;
    public float Player2SPMax;
    private GameObject P2UIGroup;

    [Header("P3")]
    public TMP_Text P3Name;
    public TMP_Text P3TargetItem;
    public Image Player3HealthBar;
    public float Player3HealthAmount;
    public TMP_Text Player3HealthAmountText;
    public float Player3HealthMax;
    public Image Player3SPBar;
    public float Player3SPAmount;
    public TMP_Text Player3SPAmountText;
    public float Player3SPMax;
    private GameObject P3UIGroup;

    [Header("Misc")]
    public Material grayScaleShader;
    public Sprite CrossDeadEffect;

    // Start is called before the first frame update
    void Start()
    {
        battleController = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
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

        Player1HealthAmountText.SetText(Player1HealthAmount.ToString());
        Player2HealthAmountText.SetText(Player2HealthAmount.ToString());
        Player3HealthAmountText.SetText(Player3HealthAmount.ToString());

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

        Player1SPAmountText.SetText(Player1SPAmount.ToString());
        Player2SPAmountText.SetText(Player2SPAmount.ToString());
        Player3SPAmountText.SetText(Player3SPAmount.ToString());

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

    /*void PlayerDeadEffect(string playerUnit)
    {
        if (playerUnit.Equals("P1"))
        {
            ChangeMaterialRecursively(P1UIGroup.GetComponent<Transform>(), grayScaleShader);
        }
        else if (playerUnit.Equals("P2"))
        {
            ChangeMaterialRecursively(P2UIGroup.GetComponent<Transform>(), grayScaleShader);
        }
        else if (playerUnit.Equals("P3"))
        {
            ChangeMaterialRecursively(P3UIGroup.GetComponent<Transform>(), grayScaleShader);
        }
    }

    void PlayerAliveEffect(Controller_CharData playerUnit)
    {
        if (playerUnit.tag.Equals("P1"))
        {
            ChangeMaterialRecursively(P1UIGroup.GetComponent<Transform>(), null);
        }
        else if (playerUnit.tag.Equals("P2"))
        {
            ChangeMaterialRecursively(P2UIGroup.GetComponent<Transform>(), null);
        }
        else if (playerUnit.tag.Equals("P3"))
        {
            ChangeMaterialRecursively(P3UIGroup.GetComponent<Transform>(), null);
        }
    }

    void ChangeMaterialRecursively(Transform parent, Material material)
    {
        // Ganti material dari GameObject
        Renderer renderer = parent.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }

        // Lakukan secara rekursif untuk tiap child
        foreach (Transform child in parent)
        {
            ChangeMaterialRecursively(child, material);
        }
    }*/
}
