using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Menu : MonoBehaviour
{
    public GameObject btnTutorial;
    public GameObject btnStage;

    public GameObject btnBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MasukStage()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MasukTutorial()
    {
        
    }

}
