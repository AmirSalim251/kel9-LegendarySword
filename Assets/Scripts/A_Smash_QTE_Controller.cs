using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A_Smash_QTE_Controller : MonoBehaviour
{
    Controller_Battle ControllerBattle;

    public float fill_Amount = 1;
    public float timer = 0;
    public bool QTE_Check = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SmashQTE() 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            fill_Amount -= .2f;
            Debug.Log("A QTE Worked");
        }

        timer += Time.deltaTime;
        if(timer > .05)
        {
            timer = 0;
            fill_Amount += .02f; 
        }
        if(fill_Amount > 1 )
        {
            fill_Amount = 1;
        }
        if(fill_Amount >= 1 )
        {
            QTE_Check = true;           
        }
        if(fill_Amount <= 0 )
        {
            ControllerBattle.Log.text = "QTE Success";
        }
        else
        {
            ControllerBattle.Log.text = "QTE Failed";
        }
        GetComponent<Image>().fillAmount = fill_Amount;
    }
}


