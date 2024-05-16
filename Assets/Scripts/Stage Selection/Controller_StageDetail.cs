using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Controller_StageDetail : MonoBehaviour
{
    public GameObject detailPanel;
    public GameObject dummyPanel;

    public TMP_Text stageName;

    public TMP_Text stageEnemyAmount;
    public TMP_Text stageEnemyName;

    public TMP_Text stageReward;
    public TMP_Text stageRewardAmount;

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
        detailPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        detailPanel.SetActive(false);
    }
}
