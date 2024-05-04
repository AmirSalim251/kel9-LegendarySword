using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
public class Script_Decrease_QTE_Controller_ : MonoBehaviour
{
    public float fill_Amount = 1;
    public float timer = 0;
    public bool QTE_Check = false;
    public TMP_Text textComponent;
    public KeyCode input;
    void Start()
    {
        textComponent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(input))
        {
            if(fill_Amount > 0)
            {
                fill_Amount -= .2f;
            }
            Debug.Log(input +  " QTE Worked");
            
        }
        timer += Time.deltaTime;
        if(timer > .05 && fill_Amount > 0)
        {
            timer = 0;
            fill_Amount += 0.02f; 
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
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
        }
        GetComponent<Image>().fillAmount = fill_Amount;
    }
}
