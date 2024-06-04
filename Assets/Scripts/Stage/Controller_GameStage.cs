using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_GameStage : MonoBehaviour
{
    public static Controller_GameStage Instance { get; private set; }
    public StageData stageChosen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
