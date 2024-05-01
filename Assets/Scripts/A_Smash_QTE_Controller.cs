using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A_Smash_QTE_Controller : MonoBehaviour
{
    Controller_Battle ControllerBattle;

    [SerializeField] public Image qteIndicator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SmashQTE() 
    {
        Debug.Log("qte mulai!");
        float timeLimit = 3f; // Adjust the time limit as desired
        float currentTime = 0f;
        float fillAmount = 0f;
        float decreaseRate = 0.2f;
        float increaseRate = 0.1f;
        ControllerBattle = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
        while (currentTime < timeLimit)
        {
            Debug.Log("masuk loop while");
            currentTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("mana key A");
                fillAmount += increaseRate;
                qteIndicator.fillAmount = fillAmount;
            }
            if (currentTime > 0.5f)
            {
                currentTime = 0;
                Debug.Log("berkurang nih");
                fillAmount -= decreaseRate;
            }

            fillAmount = Mathf.Clamp01(fillAmount);
            qteIndicator.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                ControllerBattle.Log.text = "Attack successful!";
                Debug.Log("Attack successful!");
                StartCoroutine(ControllerBattle.HeroAttack());
                qteIndicator.fillAmount = 0f;
                break;
            }
        }

        if (fillAmount < 1f)
        {
            ControllerBattle.Log.text = "Attack missed!";
            Debug.Log("Attack missed!");
            qteIndicator.fillAmount = 0f;
            ControllerBattle.PassTurn();
        }
    }
}


