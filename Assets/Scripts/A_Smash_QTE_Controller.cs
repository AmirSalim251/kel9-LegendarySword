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
    public float decreaseRate = 0.3f;
    public float increaseRate = 0.005f;

    public Image qteIndicator;

    void Awake()
    {   
        ControllerBattle = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
        qteController = GameObject.FindGameObjectWithTag("QTEController").GetComponent<A_Smash_QTE_Controller>();
        qteIndicator.fillAmount = 0f;
    }

    void Start()
    {
        fillAmount = 1f;
        currentTime = 0;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        qteIndicator.fillAmount = fillAmount;

        if (Input.GetKeyDown(KeyCode.A))
        {   
            fillAmount -= decreaseRate;
        }

        fillAmount += increaseRate;
        if (fillAmount > 1) fillAmount = 1;

        if (fillAmount <= 0f)
        {
            ControllerBattle.Log.text = "Attack successful!";
            Debug.Log("Attack successful!");

            qteIndicator.fillAmount = 0;
            
            qteController.enabled = false;
            StartCoroutine(ControllerBattle.HeroAttack());            
        }

        if (currentTime >= timeLimit)
        {
            ControllerBattle.Log.text = "Attack missed!";
            Debug.Log("Attack missed!");

            qteIndicator.fillAmount = 0;

            qteController.enabled = false;
            ControllerBattle.PassTurn();
        }
    }
}


