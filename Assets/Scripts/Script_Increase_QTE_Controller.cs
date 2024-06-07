using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Increase_QTE_Controller : MonoBehaviour
{
    public float fill_Amount = 1;
    public float timer = 0;
    public bool QTE_Check = false;
    public TMP_Text textComponent;
    public KeyCode input;
    public Image button_Before;
    public Image button_After;
    // public float increase_Value = 0;
    void Start()
    {
        textComponent.enabled = false;
        button_After.enabled = false;
        button_Before.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(input))
        {
            if(fill_Amount < 1)
            {
                fill_Amount += .2f;
            }
            button_After.enabled = true;
            button_Before.enabled = false;
            Debug.Log(input +  " QTE Worked");
        }
        if(Input.GetKeyUp(input))
        {
            button_After.enabled = false;
            button_Before.enabled = true;
        }
        timer += Time.deltaTime;
        if(timer > .05)
        {
            timer = 0;
            if(fill_Amount != 1 && fill_Amount > 0)
            {
                fill_Amount -= .02f;
            } 
        }
        if(fill_Amount > 1 )
        {
            fill_Amount = 1;
        }
        if(fill_Amount <= 0 )
        {
            QTE_Check = true;        
        }
        if(fill_Amount >= 1 )
        {
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
        }
        GetComponent<Image>().fillAmount = fill_Amount;
    }
}
