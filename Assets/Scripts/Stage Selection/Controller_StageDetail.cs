using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Controller_StageDetail : MonoBehaviour
{
    public StageData stageData;

    public GameObject detailPanel;
    public GameObject dummyPanel;

    public TMP_Text stageName;

    [Header("Enemy")]
    public TMP_Text stageEnemyAmount;
    public TMP_Text stageEnemyName;
    public Image stageEnemyIcon;

    [Header("Reward")]
    public TMP_Text stageReward;
    public TMP_Text stageRewardAmount;
    public Image stageRewardIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        UpdatePanelData();
        detailPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        stageData = null;
        detailPanel.SetActive(false);
    }

    public void UpdatePanelData()
    {
        if (stageData != null)
        {
            stageName.SetText(stageData.stageName);

            if (stageData.enemyPrefab != null)
            {
                var enemyData = stageData.enemyPrefab.GetComponent<Controller_EnemyData>();
                stageEnemyName.SetText(enemyData.monsterName);
                stageEnemyAmount.SetText("1");
                stageEnemyIcon.sprite = enemyData.monsterIcon;
            }

            if (stageData.itemReward != null)
            {
                stageReward.SetText(stageData.itemReward.itemName);
                stageRewardAmount.SetText(stageData.itemAmount.ToString());
                stageRewardIcon.sprite = stageData.itemReward.itemIcon;
            }
        }
    }

    public void EnterButton()
    {
        Controller_GameStage.Instance.stageChosen = stageData;
        SceneManager.LoadScene("VN Scene");
    }


}
