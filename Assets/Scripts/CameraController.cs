using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Controller_Battle battleController;
    public CinemachineVirtualCamera[] virtualCameras;
    
    private CinemachineVirtualCamera targetCamera;
    private BattleState currentState;

    void Update()
    {
        if(currentState == battleController.state) return;

        if (battleController.state == BattleState.PLAYER1TURN)
        {
            targetCamera = GameObject.Find("AlexCam").GetComponent<CinemachineVirtualCamera>();
            currentState = BattleState.PLAYER1TURN;
        }

        else if (battleController.state == BattleState.PLAYER2TURN)
        {
            targetCamera = GameObject.Find("FreyaCam").GetComponent<CinemachineVirtualCamera>();
            currentState = BattleState.PLAYER2TURN;
        }

        else if (battleController.state == BattleState.PLAYER3TURN)
        {
            targetCamera = GameObject.Find("MagnusCam").GetComponent<CinemachineVirtualCamera>();
            currentState = BattleState.PLAYER3TURN;
        }        
        
        else if (battleController.state == BattleState.ENEMYTURN)
        {
            targetCamera = GameObject.Find("EnemyCam").GetComponent<CinemachineVirtualCamera>();
            currentState = BattleState.ENEMYTURN;
        }        

        SwitchToCamera(targetCamera);
    }

    void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras) camera.enabled = camera == targetCamera;
    }    
}
