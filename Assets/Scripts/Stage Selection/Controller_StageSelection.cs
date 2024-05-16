using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_StageSelection : MonoBehaviour
{
    public GameObject playerSprite;

    public GameObject btnStageZero;
    public GameObject btnStage1;
    public GameObject btnStage2;

    public GameObject activeStage;
    // Start is called before the first frame update
    void Start()
    {
        activeStage = btnStageZero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPlayerDelta()
    {
        var playerPosDelta = new Vector3();
        /*playerPosDelta = playerSprite.GetComponent<Vector3>() - activeStage.GetComponent<Vector3>();*/
        playerPosDelta = playerSprite.transform.position - activeStage.transform.position;
        return playerPosDelta;
    }

    public void MovePlayerPosition(GameObject nextStage)
    {
        playerSprite.transform.position = GetPlayerDelta() + nextStage.transform.position;
        GetPlayerDelta();
    }
}
