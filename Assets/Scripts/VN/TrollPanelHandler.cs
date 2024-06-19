using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrollPanel : MonoBehaviour
{
    private Animator anim;
    
    public Button skipButton;
    public string[] messages = 
    {
        "",
        "Please do not skip the story",
        "I've told you, DO NOT SKIP THE STORY",
        "Press that button one more time and i'll crash the game"    
    };
    
    public TextMeshProUGUI text;
    public int idx = -1;

    void Start() 
    {
        anim = gameObject.GetComponent<Animator>();
        skipButton.onClick.AddListener(RunAnim);

    }

    void RunAnim()
    {
        idx++;
        text.text = messages[idx];
        anim.SetTrigger("RunIt");
    }
}
