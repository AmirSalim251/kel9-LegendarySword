using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_CommandPanel : MonoBehaviour
{
    public GameObject btnATK;
    public Sprite btnATKBefore;
    public Sprite btnATKAfter;

    public GameObject btnDEF;
    public Sprite btnDEFBefore;
    public Sprite btnDEFAfter;

    public GameObject btnSK;
    public Sprite btnSKBefore;
    public Sprite btnSKAfter;

    public GameObject btnIT;
    public Sprite btnITBefore;
    public Sprite btnITAfter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetButtonSprite()
    {
        btnATK.GetComponent<Image>().sprite = btnATKBefore;
        btnDEF.GetComponent<Image>().sprite = btnDEFBefore;
        btnSK.GetComponent<Image>().sprite = btnSKBefore;
        btnIT.GetComponent<Image>().sprite = btnITBefore;
    }

    public void ChangeStateButton(GameObject button)
    {

    }

}
