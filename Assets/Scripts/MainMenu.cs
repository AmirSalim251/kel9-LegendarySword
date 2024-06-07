using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelStart;
    public GameObject panelMenu;
    public GameObject panelSettings;

    public Button buttonNewGame;
    public Button buttonLoadGame;

    public Button buttonQuit;
    public Button buttonCredits;
    public Button buttonSettings;

    private bool isSettingOpen = false;

    public void Start()
    {

        if(SessionManager.isNewSession)
        {
            PlayerPrefs.DeleteAll();
            panelStart.SetActive(true);
            HideMenuUI();
        }
        else
        {
            panelStart.SetActive(false);
        }

        //setup
        //button New Game
        AddEvent(buttonNewGame.gameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonNewGame.gameObject); });
        AddEvent(buttonNewGame.gameObject, EventTriggerType.PointerClick, delegate { OnClick(buttonNewGame.gameObject); });

        //button Load Game
        AddEvent(buttonLoadGame.gameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonLoadGame.gameObject); });
        AddEvent(buttonLoadGame.gameObject, EventTriggerType.PointerClick, delegate { OnClick(buttonLoadGame.gameObject); });

        //button Credits
        AddEvent(buttonCredits.gameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonCredits.gameObject); });
        AddEvent(buttonCredits.gameObject, EventTriggerType.PointerClick, delegate { OnClick(buttonCredits.gameObject); });

        //buttonSettings
        AddEvent(buttonSettings.gameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonSettings.gameObject); });
        AddEvent(buttonSettings.gameObject, EventTriggerType.PointerClick, delegate { OnClick(buttonSettings.gameObject); });

        //buttonQuit
        AddEvent(buttonQuit.gameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonQuit.gameObject); });
        AddEvent(buttonQuit.gameObject, EventTriggerType.PointerClick, delegate { OnClick(buttonQuit.gameObject); });
    }

    public void Update()
    {
        if(SessionManager.isNewSession)
        {
            if (Input.anyKeyDown)
            {
                SessionManager.isNewSession = false;
                AudioManager.Instance.PlaySFX("buttonPressed");
                StartCoroutine(ExitTitleScreen());
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SimulateButtonPress(buttonQuit));
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(SimulateButtonPress(buttonCredits));
            GoToCredits();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(SimulateButtonPress(buttonSettings));
            ButtonSettings();
        }
    }

    public void OnEnter(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonHover");
    }

    public void OnClick(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonPressed");
    }

    public void playGame()
    {
        SceneTransition.instance.LoadScene("StageSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneTransition.instance.LoadScene("Credits");
    }

    public void ButtonSettings()
    {
        AudioManager.Instance.PlaySFX("buttonPressed");
        if(!isSettingOpen)
        {
            OpenSettings();
        }
        else
        {
            CloseSettings();
        }
    }

    

    public void OpenSettings() 
    { 
        CanvasGroup settingsGroup = panelSettings.GetComponent<CanvasGroup>();
        settingsGroup.alpha = 1.0f; // Make UI element visible
        settingsGroup.interactable = true; // Enable interaction
        settingsGroup.blocksRaycasts = true; // Enable raycasting
        isSettingOpen = true;
    }

    public void CloseSettings()
    {
        CanvasGroup settingsGroup = panelSettings.GetComponent<CanvasGroup>();
        settingsGroup.alpha = 0f; // Make UI element invisible
        settingsGroup.interactable = false; // Disable interaction
        settingsGroup.blocksRaycasts = false; // Disable raycasting
        isSettingOpen = false;
    }

    public void HideMenuUI()
    {
        CanvasGroup menuGroup = panelMenu.GetComponent<CanvasGroup>();
        menuGroup.alpha = 0f; // Make UI element invisible
        menuGroup.interactable = false; // Disable interaction
        menuGroup.blocksRaycasts = false; // Disable raycasting
    }

    public void ShowMenuUI()
    {
        CanvasGroup menuGroup = panelMenu.GetComponent<CanvasGroup>();
        menuGroup.alpha = 1.0f; // Make UI element visible
        menuGroup.interactable = true; // Enable interaction
        menuGroup.blocksRaycasts = true; // Enable raycasting
    }

    IEnumerator ExitTitleScreen()
    {
        ShowMenuUI();
        panelStart.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1f);
        panelStart.SetActive(false);
        /*ShowMenuUI();*/
    }


    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    IEnumerator SimulateButtonPress(Button button)
    {
        ColorBlock colors = button.colors;

        // Change to pressed color
        button.image.CrossFadeColor(colors.pressedColor, colors.fadeDuration, true, true);
        yield return new WaitForSeconds(colors.fadeDuration);

        // Change back to normal color
        button.image.CrossFadeColor(colors.normalColor, colors.fadeDuration, true, true);
    }
}

public static class SessionManager
{
    public static bool isNewSession = true;
    public static bool isNewInven = true;
}
