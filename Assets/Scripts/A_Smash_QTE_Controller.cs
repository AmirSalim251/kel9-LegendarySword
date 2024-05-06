using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A_Smash_QTE_Controller : MonoBehaviour
{
    A_Smash_QTE_Controller qteController;
    
    Controller_Battle ControllerBattle;
    public float currentTime = 0f;
    public float timeLimit = 10f;
    public float fillAmount = 0;
    public float decreaseRate = 0.05f;
    public float increaseRate = 0.2f;

    public Image qteIndicator;
    public Image qteButton;

    void Awake()
    {   
        ControllerBattle = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
        qteController = GameObject.FindGameObjectWithTag("QTEController").GetComponent<A_Smash_QTE_Controller>();
        qteIndicator.fillAmount = 0f;
    }

    void OnEnable()
    {
        currentTime = 0;
        fillAmount = 0;
        qteButton.enabled = true;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {   
            fillAmount += increaseRate;
            qteIndicator.fillAmount = fillAmount;
        }

        fillAmount -= decreaseRate;
        qteIndicator.fillAmount = fillAmount;

        if (fillAmount >= 1f)
        {
            ControllerBattle.Log.text = "Attack successful!";

            qteIndicator.fillAmount = 0;
            
            qteController.enabled = false;
            qteButton.enabled = false;
            StartCoroutine(ControllerBattle.HeroAttack());            
        }

        if (currentTime >= timeLimit)
        {
            ControllerBattle.Log.text = "Attack missed!";
            Debug.Log("Attack missed!");

            qteIndicator.fillAmount = 0;

            qteController.enabled = false;
            qteButton.enabled = false;
            ControllerBattle.PassTurn();
        }
    }
}


