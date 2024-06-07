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

    public KeyCode input;

    public Image qteIndicator;
    public Image button_Before;
    public Image button_After;

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
        button_Before.enabled = true;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(input))
        {   
            fillAmount += increaseRate;
            qteIndicator.fillAmount = fillAmount;
            button_After.enabled = true;
            button_Before.enabled = false;
        }

        fillAmount -= decreaseRate;
        qteIndicator.fillAmount = fillAmount;

        if (fillAmount < 0f)
        {
            fillAmount = 0f;
        }

        if(Input.GetKeyUp(input))
        {
            button_After.enabled = false;
            button_Before.enabled = true;
        }

        if (fillAmount >= 1f)
        {
            ControllerBattle.Log.text = "Attack successful!";

            qteIndicator.fillAmount = 0;
            
            qteController.enabled = false;
            button_Before.enabled = false;
            button_After.enabled = false;
            StartCoroutine(ControllerBattle.RTTime());            
        }

        if (currentTime >= timeLimit)
        {
            ControllerBattle.Log.text = "Attack missed!";
            Debug.Log("Attack missed!");

            qteIndicator.fillAmount = 0;

            qteController.enabled = false;
            button_Before.enabled = false;
            button_After.enabled = false;
            ControllerBattle.PassTurn();
        }
    }
}


