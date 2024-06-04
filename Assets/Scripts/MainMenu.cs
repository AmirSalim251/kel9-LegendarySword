using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject panelSettings;

    public Button buttonNewGame;
    public Button buttonLoadGame;

    public Button buttonQuit;
    public Button buttonCredits;
    public Button buttonSettings;

    private bool isSettingOpen = false;

    public void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            buttonQuit.onClick.Invoke();
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            buttonCredits.OnPointerDown(new PointerEventData(EventSystem.current));
            buttonCredits.OnSubmit(new BaseEventData(EventSystem.current));
            buttonCredits.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            buttonCredits.OnPointerUp(new PointerEventData(EventSystem.current));

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonSettings.onClick.Invoke();
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
        SceneManager.LoadScene("StageSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
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

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

}
