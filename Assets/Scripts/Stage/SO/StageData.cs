using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/StageData")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public string stageName; // Name of the stage
    public string description; // Description of the stage
    public int stageID; // Identifier for the stage

    [Header("Story Script")]
    public TextAsset storyStart;
    public TextAsset storyMid;
    public TextAsset storyEnd;

    [Header("Enemies")]
    public GameObject enemyPrefab;
    public int enemyID;

    [Header("Rewards")]
    public BaseItem itemReward;
    public int itemAmount;

    [Header("Score")]

    public List<StageObjective> stageObjectives;

    /*public ScoreType score1req;
    public string score1desc;
    public int score1threshold;

    public ScoreType score2req;
    public string score2desc;
    public int score2threshold;
    

    public ScoreType score3req;
    public string score3desc;
    public int score3threshold;

    public ScoreType GetScoreReq(int scoreReq)
    {
        ScoreType score;
        if(scoreReq == 1){
            score = score1req;
        }
        else if(scoreReq == 2)
        {
            score = score2req;
        }
        else if(scoreReq == 3)
        {
            score = score3req;
        }
        else
        {
            score = 0;
        }

        return score;
    }*/

    [Header("Completion")]
    public bool isCleared;
}

public enum ScoreType
{
    TurnTaken,
    HPLost,
    NoCharDeath,
    HPUnder
}

[System.Serializable]
public class StageObjective
{
    public ScoreType objective;
    public int objectiveReq;
    public string objectiveDesc;
}
