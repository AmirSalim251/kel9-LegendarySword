using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pausePanel;
    private bool isPaused;

    public GameObject settingsPanel;
    private bool isSettingOpen;

    public GameObject combatUI;
    public GameObject endPanel;

    public GameObject transitionPanel;

    [Space]
    public Controller_Score scoreController;

    [Header("Combat Buttons")]
    public GameObject buttonAttack;
    public GameObject buttonDefend;
    public GameObject buttonItem;
    public GameObject buttonSkill;

    [Header("Settings Buttons")]
    public GameObject buttonPause;
    public GameObject buttonResume;
    public GameObject buttonMainMenu;
    public GameObject buttonSettings;

    [Header("Enemy")]
    public GameObject monster;
    public Vector3 monsterSpawnPos;

    [Header("Players")]
    public GameObject hero;
    public GameObject heroSpawnPos;

    public GameObject partyMem1;
    public Vector3 partyMem1SpawnPos;

    public GameObject partyMem2;
    public Vector3 partyMem2SpawnPos;

    [Header("Stage Info")]
    public StageData currentStage;
    public int stageID;

    /*[Header("Scoring Text")]
    public TMP_Text textScore1;
    public TMP_Text textScore2;
    public TMP_Text textScore3;*/

    /* public Controller_CharData charDataController;

     public static Model_CharData charDataAlex;
     public Model_CharData charDataFreya;
     public Model_CharData charDataMagnus;*/



    // Start is called before the first frame update

    void Awake()
    {
        LoadStageData();
        GenerateMonster();
    }
    void Start()
    {
        SetupButtonTrigger();

        /*charDataAlex = LoadCharData();*/
        isPaused = false;
        isSettingOpen = false;
        /*LoadStageData();*/

        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<Controller_Score>();

        if(scoreController != null )
        {
            scoreController.stageData = currentStage;
            scoreController.LoadStageData();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonPauseAction();
        }

        if (endPanel.activeSelf && Input.anyKeyDown)
        {
            BackToMenu();
        }
    }

    public void SetupButtonTrigger()
    {
        //COMMAND
        //button Attack
        AddEvent(buttonAttack, EventTriggerType.PointerEnter, delegate { OnEnter(buttonAttack); });
        AddEvent(buttonAttack, EventTriggerType.PointerClick, delegate { OnClick(buttonAttack); });

        //button Defend
        AddEvent(buttonDefend, EventTriggerType.PointerEnter, delegate { OnEnter(buttonDefend); });
        AddEvent(buttonDefend, EventTriggerType.PointerClick, delegate { OnClick(buttonDefend); });

        //button Item
        AddEvent(buttonItem, EventTriggerType.PointerEnter, delegate { OnEnter(buttonItem); });
        AddEvent(buttonItem, EventTriggerType.PointerClick, delegate { OnClick(buttonItem); });

        //button Skill
        AddEvent(buttonSkill, EventTriggerType.PointerEnter, delegate { OnEnter(buttonSkill); });
        AddEvent(buttonSkill, EventTriggerType.PointerClick, delegate { OnClick(buttonSkill); });

        //PAUSE
        //button Pause
        AddEvent(buttonPause, EventTriggerType.PointerEnter, delegate { OnEnter(buttonPause); });
        AddEvent(buttonPause, EventTriggerType.PointerClick, delegate { OnClick(buttonPause); });

        //button Resume
        AddEvent(buttonResume, EventTriggerType.PointerEnter, delegate { OnEnter(buttonResume); });
        AddEvent(buttonResume, EventTriggerType.PointerClick, delegate { OnClick(buttonResume); });

        //button Main Menu
        AddEvent(buttonMainMenu, EventTriggerType.PointerEnter, delegate { OnEnter(buttonMainMenu); });
        AddEvent(buttonMainMenu, EventTriggerType.PointerClick, delegate { OnClick(buttonMainMenu); });

        //button Settings
        AddEvent(buttonSettings, EventTriggerType.PointerEnter, delegate { OnEnter(buttonSettings); });
        AddEvent(buttonSettings, EventTriggerType.PointerClick, delegate { OnClick(buttonSettings); });

    }

    public void OnEnter(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonHover");
    }

    public void OnClick(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonPressed");
    }


    public void GenerateMonster()
    {
        if(monster != null)
        {
            Instantiate(monster, monsterSpawnPos, Quaternion.Euler(0,90,0));
        }
    }

    public void GenerateChars() 
    {
        
    }

    public void ButtonPauseAction()
    {
        if (isPaused)
        {
            if (!isSettingOpen)
            {
                ContinueGame();
                Debug.Log("Game should NOT be paused rn");
            }
            else
            {
                CloseSettings();
            }
        }
        else
        {
            PauseGame();
            Debug.Log("Game should be paused rn");
        }
        /*buttonPause.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();*/
    }

    public void PauseGame()
    {
        OpenPausePanel();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ContinueGame()
    {
        HidePausePanel();
        Time.timeScale = 1;
        isPaused = false;
    }

    public void OpenPausePanel()
    {
        CanvasGroup pauseGroup = pausePanel.GetComponent<CanvasGroup>();
        pauseGroup.alpha = 1.0f; // Make UI element visible
        pauseGroup.interactable = true; // Enable interaction
        pauseGroup.blocksRaycasts = true; // Enable raycasting
    }

    public void HidePausePanel()
    {
        CanvasGroup pauseGroup = pausePanel.GetComponent<CanvasGroup>();
        pauseGroup.alpha = 0f; // Make UI element visible
        pauseGroup.interactable = false; // Enable interaction
        pauseGroup.blocksRaycasts = false; // Enable raycasting
    }

    public void OpenSettings()
    {
        CanvasGroup settingsGroup = settingsPanel.GetComponent<CanvasGroup>();
        settingsGroup.alpha = 1.0f; // Make UI element visible
        settingsGroup.interactable = true; // Enable interaction
        settingsGroup.blocksRaycasts = true; // Enable raycasting
        isSettingOpen = true;
    }

    public void CloseSettings()
    {
        CanvasGroup settingsGroup = settingsPanel.GetComponent<CanvasGroup>();
        settingsGroup.alpha = 0f; // Make UI element invisible
        settingsGroup.interactable = false; // Disable interaction
        settingsGroup.blocksRaycasts = false; // Disable raycasting
        isSettingOpen = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneTransition.instance.LoadScene("StageSelection");
    }

    public void saveController(Model_CharData charData)
    {
        // Konversi objek CharDataModel menjadi string JSON
        string json = JsonConvert.SerializeObject(charData, Formatting.Indented);

        // Tentukan path file tempat menyimpan data
        // Ubah "path/to/your/savefile.json" sesuai dengan lokasi penyimpanan yang diinginkan
        string filePath = "Assets/SaveData/savefile.json";

        // Tulis data JSON ke dalam file
        File.WriteAllText(filePath, json);
    }

    public static Model_CharData LoadCharData()
    {
        // Tentukan path file tempat data disimpan
        string filePath = "Assets/SaveData/savefile.json";

        if (File.Exists(filePath))
        {
            // Baca semua teks dari file
            string json = File.ReadAllText(filePath);

            // Konversi string JSON kembali menjadi objek CharDataModel
            Model_CharData charData = JsonConvert.DeserializeObject<Model_CharData>(json);
            return charData;
        }
        else
        {
            Debug.Log("Gagal load");
        }
        return null;

    }

    public void LoadStageData()
    {
        if(Controller_GameStage.Instance != null)
        {
            currentStage = Controller_GameStage.Instance.stageChosen;
        }
        
        monster = currentStage.enemyPrefab;
        stageID = currentStage.stageID;
    }



    /*public void LoadStageData()
    {
        for(int i = 1; i < 4; i++)
        {
            ScoreType score;
            score = currentStage.GetScoreReq(i);
            if(score == ScoreType.HPLost)
            {
                textScore1.SetText(score.ToString());
            }
            else if(score == ScoreType.TurnTaken)
            {
                textScore2.SetText(score.ToString());
            }
            else
            {
                textScore3.SetText(score.ToString());
            }
        }
        
    }*/

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
