using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Single_QTE_System : MonoBehaviour
{
    public GameObject display_Button;
    public GameObject status_Box;
    public int QTE_Generate;
    public int waiting_Key_Pressed;
    public int correct_Key;
    public int counting_Down;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waiting_Key_Pressed == 0)
        {
            QTE_Generate = Random.Range(1, 5);
            counting_Down = 1;
            StartCoroutine(countdown_Time());

            if(QTE_Generate == 1)
            {
                waiting_Key_Pressed = 1;
                display_Button.GetComponent<Text>().text = "A";
            }
            if(QTE_Generate == 2)
            {
                waiting_Key_Pressed = 1;
                display_Button.GetComponent<Text>().text = "W";
            }
            if(QTE_Generate == 3)
            {
                waiting_Key_Pressed = 1;
                display_Button.GetComponent<Text>().text = "S";
            }
            if(QTE_Generate == 4)
            {
                waiting_Key_Pressed = 1;
                display_Button.GetComponent<Text>().text = "D";
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
    }

    IEnumerator KeyPressing()
    {
        QTE_Generate = 5;
        if( correct_Key == 1)
        {
            counting_Down = 2;
            status_Box.GetComponent<Text>().text = "Pass!";
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            status_Box.GetComponent<Text>().text = "";
            display_Button.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
        if( correct_Key == 2)
        {
            counting_Down = 2;
            status_Box.GetComponent<Text>().text = "Fail";
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            status_Box.GetComponent<Text>().text = "";
            display_Button.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
    }

    IEnumerator countdown_Time()
    {
        yield return new WaitForSeconds(2f);
        if(counting_Down == 1) 
        {
            QTE_Generate = 4;
            counting_Down = 2;
            status_Box.GetComponent<Text>().text = "Fail!!!";
            yield return new WaitForSeconds(1.5f);
            correct_Key = 0;
            status_Box.GetComponent<Text>().text = "";
            display_Button.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            waiting_Key_Pressed = 0;
            counting_Down = 1;
        }
    }
}
