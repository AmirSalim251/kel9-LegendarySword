using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Smash_QTE_Controller : MonoBehaviour
{
    Smash_QTE_Controller qteController;
    Controller_Battle ControllerBattle;

    [Header("Time")]
    public float currentTime = 0f;
    public float timeLimit = 10f;
    public float fillAmount = 0;
    public float decreaseRate = 0.05f;
    public float increaseRate = 0.2f;

    [Header("QTE Indicator")]
    public Image qteIndicator;
    public KeyCode input;
    
    [Header("Keys")]
    public Image buttonWPressed;
    public Image buttonWNormal;
    public Image buttonAPressed;
    public Image buttonANormal;
    public Image buttonSPressed;
    public Image buttonSNormal;
    public Image buttonDPressed;
    public Image buttonDNormal;

    private KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    

    void Awake()
    {   
        ControllerBattle = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();
        qteController = GameObject.FindGameObjectWithTag("QTEController").GetComponent<Smash_QTE_Controller>();
        qteIndicator.fillAmount = 0f;
    }

    void OnEnable()
    {
        currentTime = 0;
        fillAmount = 0;
        RandomizeInput();
        UpdateButtonVisuals();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(input))
        {
            fillAmount += increaseRate;
            qteIndicator.fillAmount = fillAmount;
            AudioManager.Instance.PlaySFX("qteTrigger");
            SetButtonPressedVisuals(true);
        }
        if(Input.GetKeyUp(input))
        {
            SetButtonPressedVisuals(false);
        }

        fillAmount -= decreaseRate * Time.deltaTime;
        qteIndicator.fillAmount = fillAmount;
        
        if (fillAmount < 0)
        {
            fillAmount = 0;
        }

        if (fillAmount >= 1f)
        {
            ControllerBattle.Log.text = "Attack successful!";

            qteIndicator.fillAmount = 0;
            ResetController();

            if (ControllerBattle.isAlexUsingSkill == true)
            {
                ControllerBattle.isAlexUsingSkill = false;
                StartCoroutine(ControllerBattle.AlexFirstSkill());
            } 
            else
                StartCoroutine(ControllerBattle.RTTime());                        
        }

        if (currentTime >= timeLimit)
        {
            ControllerBattle.Log.text = "Attack missed!";
            Debug.Log("Attack missed!");

            qteIndicator.fillAmount = 0;

            ResetController();

            ControllerBattle.isAlexUsingSkill = false;
            ControllerBattle.PassTurn();
        }
    }

    void RandomizeInput()
    {
        input = keys[Random.Range(0, keys.Length)];
    }

    void UpdateButtonVisuals()
    {
        buttonWNormal.enabled = input == KeyCode.W;
        buttonANormal.enabled = input == KeyCode.A;
        buttonSNormal.enabled = input == KeyCode.S;
        buttonDNormal.enabled = input == KeyCode.D;
        buttonWPressed.enabled = false;
        buttonAPressed.enabled = false;
        buttonSPressed.enabled = false;
        buttonDPressed.enabled = false;
    }

    void SetButtonPressedVisuals(bool isPressed)
    {
        if (input == KeyCode.W)
        {
            buttonWPressed.enabled = isPressed;
            buttonWNormal.enabled = !isPressed;
        }
        if (input == KeyCode.A)
        {
            buttonAPressed.enabled = isPressed;
            buttonANormal.enabled = !isPressed;
        }
        if (input == KeyCode.S)
        {
            buttonSPressed.enabled = isPressed;
            buttonSNormal.enabled = !isPressed;
        }
        if (input == KeyCode.D)
        {
            buttonDPressed.enabled = isPressed;
            buttonDNormal.enabled = !isPressed;
        }
    }

    void ResetController()
    {
        qteController.enabled = false;
        buttonWNormal.enabled = false;
        buttonWPressed.enabled = false;
        buttonANormal.enabled = false;
        buttonAPressed.enabled = false;
        buttonSNormal.enabled = false;
        buttonSPressed.enabled = false;
        buttonDNormal.enabled = false;
        buttonDPressed.enabled = false;
    }
}


