using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Score : MonoBehaviour
{
    public Controller_InventoryManager inventoryManager;
    public GameObject endPanel;

    public StageData stageData;
    public Controller_Battle battleData;

    public Sprite objectiveImgSuccess;
    public Sprite objectiveImgFailed;

    [Header("Objectives")]
    public List<StageObjective> stageObjectives;
    /*public ScoreType scoreType1;
    public ScoreType scoreType2;
    public ScoreType scoreType3;*/

    /*public int score1reqAmount;
    public int score2reqAmount;
    public int score3reqAmount;*/

    [Header("Scoring Text")]
    public List<TMP_Text> textScore;
    /*public TMP_Text textScore1;
    public TMP_Text textScore2;
    public TMP_Text textScore3;*/

    [Header("Result Panel Text")]
    public List<TMP_Text> objectiveText;
    /*public TMP_Text resultObj1;
    public TMP_Text resultObj2;
    public TMP_Text resultObj3;*/

    public List<TMP_Text> objectiveResultText;
    /*public TMP_Text result1;
    public TMP_Text result2;
    public TMP_Text result3;*/

    public List<TMP_Text> objectiveReqText;
    /*public TMP_Text resultReq1;
    public TMP_Text resultReq2;
    public TMP_Text resultReq3;*/

    public List<Image> objectiveCheque;

    private string rewardName;
    public TMP_Text rewardNameText;
    private int rewardAmount;
    public TMP_Text rewardAmountText;
    public Image rewardIcon;

    private int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        /*inventoryManager = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<Controller_InventoryManager>();
        CheckScore();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStageData()
    {
        /*for (int i = 1; i < 4; i++)
        {
            ScoreType score;
            score = stageData.GetScoreReq(i);
            if (score == ScoreType.HPLost)
            {
                textScore1.SetText(score.ToString());
            }
            else if (score == ScoreType.TurnTaken)
            {
                textScore2.SetText(score.ToString());
            }
            else
            {
                textScore3.SetText(score.ToString());
            }
        }*/

        for (int i = 0; i < stageData.stageObjectives.Count; i++)
        {
            stageObjectives.Add(stageData.stageObjectives[i]);
        }

        /*scoreType.Add(stageData.score1req);
        scoreType.Add(stageData.score2req);
        scoreType.Add(stageData.score3req);
*/
        /*scoreType1 = stageData.score1req;
        scoreType2 = stageData.score2req;
        scoreType3 = stageData.score3req;*/

        for (int i = 0; i < stageObjectives.Count; i++)
        {
            textScore[i].SetText(stageObjectives[i].objectiveDesc);

            objectiveText[i].SetText(stageObjectives[i].objectiveDesc);
            objectiveReqText[i].SetText(stageObjectives[i].objectiveReq.ToString());
        }

        /*score1reqAmount = stageData.score1threshold;
        score2reqAmount = stageData.score2threshold;
        score3reqAmount = stageData.score3threshold;


        textScore1.SetText(stageData.score1desc);
        textScore2.SetText(stageData.score2desc);
        textScore3.SetText(stageData.score3desc);

        resultObj1.SetText(stageData.score1desc);
        resultObj2.SetText(stageData.score2desc);
        resultObj3.SetText(stageData.score3desc);

        resultReq1.SetText(stageData.score1threshold.ToString());
        resultReq2.SetText(stageData.score2threshold.ToString());
        resultReq3.SetText(stageData.score3threshold.ToString());*/

        rewardName = stageData.itemReward.itemName;
        rewardNameText.SetText(rewardName.ToString());

        rewardAmount = stageData.itemAmount;
        rewardAmountText.SetText(stageData.itemAmount.ToString());

        rewardIcon.sprite = stageData.itemReward.itemIcon;
        
    }

    public void CheckScore()
    {
        int turnTaken = battleData.turnCount;
        var hpLost = battleData.totalHPDelta;
        int totalCharDead = battleData.totalCharDead;

        for(int i = 0; i < stageObjectives.Count; i++)
        {
            if (stageObjectives[i].objective == ScoreType.TurnTaken)
            {
                objectiveResultText[i].SetText(turnTaken.ToString());
                if (turnTaken <= stageObjectives[i].objectiveReq)
                {
                    objectiveCheque[i].sprite = objectiveImgSuccess;
                    totalScore++;
                }
            }
            else if (stageObjectives[i].objective == ScoreType.HPLost)
            {
                objectiveResultText[i].SetText(hpLost.ToString());
                if (hpLost <= stageObjectives[i].objectiveReq)
                {
                    objectiveCheque[i].sprite = objectiveImgSuccess;
                    totalScore++;
                }
                Debug.Log(hpLost.ToString());
            }
            else if (stageObjectives[i].objective == ScoreType.NoCharDeath)
            {
                objectiveResultText[i].SetText(totalCharDead.ToString());
                if (totalCharDead <= stageObjectives[i].objectiveReq)
                {
                    objectiveCheque[i].sprite = objectiveImgSuccess;
                    totalScore++;
                }
            }
        }

        rewardAmount *= totalScore;
        rewardAmountText.SetText(rewardAmount.ToString());

        //add reward to inven

        inventoryManager.AddItem(inventoryManager.itemDictionary.GetValueByKey(rewardName.ToString()), rewardAmount);

        inventoryManager.SaveInventory();

        /*if(scoreType1 == ScoreType.TurnTaken)
        {
            result1.SetText(turnTaken.ToString());

        }
        else if(scoreType1 == ScoreType.HPLost)
        {
            result1.SetText(hpLost.ToString());
        }
        else if(scoreType1 == ScoreType.NoCharDeath)
        {
            result1.SetText(anyCharDead.ToString());
        }*/
    }
}
