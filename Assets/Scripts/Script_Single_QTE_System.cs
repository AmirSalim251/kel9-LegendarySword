using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Single_QTE_System : MonoBehaviour
{
    // public Image display_Button;
    public GameObject status_Box;
    private int QTE_Generate;
    private int waiting_Key_Pressed;
    private int correct_Key;
    private int counting_Down;
    public Image[] button_Images;
    //Image Index
    //0 = A_button
    //1 = W_button
    //2 = S_button
    //3 = D_button
    //4 = Left_mouse_button
    //5 = Right_mouse_button

    void Start()
    {
        Disbale_Iamge();
    }

    void Update()
    {
        
        if(waiting_Key_Pressed == 0)
        {
            QTE_Generate = Random.Range(1, 7);
            counting_Down = 1;
            StartCoroutine(countdown_Time());

            if(QTE_Generate == 1)
            {
                waiting_Key_Pressed = 1;
                button_Images[0].enabled = true;
                Debug.Log("A BUTTON");
            }
            if(QTE_Generate == 2)
            {
                waiting_Key_Pressed = 1;
                button_Images[1].enabled = true;
                Debug.Log("W BUTTON");
            }
            if(QTE_Generate == 3)
            {
                waiting_Key_Pressed = 1;
                button_Images[2].enabled = true;
                Debug.Log("S BUTTON");
            }
            if(QTE_Generate == 4)
            {
                waiting_Key_Pressed = 1;
                button_Images[3].enabled = true;
                Debug.Log("D BUTTON");
            }
            if(QTE_Generate == 5)
            {
                waiting_Key_Pressed = 1;
                button_Images[4].enabled = true;
                Debug.Log("LEFT BUTTON");
            }
            if(QTE_Generate == 6)
            {
                waiting_Key_Pressed = 1;
                button_Images[5].enabled = true;
                Debug.Log("RIGHT BUTTON");
            }
        }
        if(QTE_Generate == 1)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetButtonDown("A_QTE_Key"))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if(QTE_Generate == 2)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetButtonDown("W_QTE_Key"))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if(QTE_Generate == 3)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetButtonDown("S_QTE_Key"))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if(QTE_Generate == 4)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetButtonDown("D_QTE_Key"))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if(QTE_Generate == 5)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetMouseButtonDown(0))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if(QTE_Generate == 6)
        {
            if(Input.anyKeyDown) 
            {
                if(Input.GetMouseButtonDown(1))
                {
                    correct_Key = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correct_Key = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        QTE_Generate = 7;
        if( correct_Key == 1)
        {
            counting_Down = 2;
            status_Box.GetComponent<TextMeshProUGUI>().text = "Pass!";
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            Disbale_Iamge();
            status_Box.GetComponent<TextMeshProUGUI>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
        if( correct_Key == 2)
        {
            counting_Down = 2;
            status_Box.GetComponent<TextMeshProUGUI>().text = "Fail";
            Debug.Log("Fail Wrong Press");
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            Disbale_Iamge();
            status_Box.GetComponent<TextMeshProUGUI>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
    }

    IEnumerator countdown_Time()
    {
        yield return new WaitForSeconds(3f);
        if(counting_Down == 1) 
        {
            QTE_Generate = 5;
            counting_Down = 2;
            status_Box.GetComponent<TextMeshProUGUI>().text = "Fail!!!";
            Debug.Log("Fail Run Out of Time");
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            Disbale_Iamge();
            status_Box.GetComponent<TextMeshProUGUI>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
    }

    void Disbale_Iamge(){
        foreach (Image img in button_Images)
        {
            img.enabled = false;
        }
    }
}
