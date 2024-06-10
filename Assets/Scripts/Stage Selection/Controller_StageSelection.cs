using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller_StageSelection : MonoBehaviour
{
    public Controller_StageDetail stageDetail;

    public GameObject playerSprite;
    private Animator animator;

    public GameObject btnStageZero;
    public GameObject btnStage1;
    public GameObject btnStage2;

    public GameObject activeStage;

    [Header("Buttons")]
    public GameObject btnInventory;
    public GameObject btnBack;
    public GameObject btnConfirm;
    public GameObject btnDepart;

    private float speed = 60f;
    // Start is called before the first frame update
    void Start()
    {

        //get animator
        animator = playerSprite.GetComponent<Animator>();

        //create gamestage dontdestroy
        Controller_GameStage.Instance.stageChosen = null;
        activeStage = btnStageZero;

        //setup button EventTrigger
        SetupButtonTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller_GameStage.Instance.stageChosen != null)
        {

            btnConfirm.SetActive(true);
        }
        else
        {
            
            btnConfirm.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            StartCoroutine(SimulateButtonPress(btnBack.GetComponent<Button>()));
            BackToMenu();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            var isStage = EnterStage();
            if (activeStage.GetComponent<Controller_StagePanel>().stageData != null)
            {
                isStage = true;
            }
            else
            {
                isStage = false;
            }

            if (isStage)
            {
                StartCoroutine(SimulateButtonPress(btnConfirm.GetComponent<Button>()));
                /*stageDetailController.stageData = stageData;*/
                stageDetail.OpenPanel();
            }
        }
    }

    public void SetupButtonTrigger()
    {
        //button Inventory
        AddEvent(btnInventory, EventTriggerType.PointerEnter, delegate { OnEnter(btnInventory); });
        AddEvent(btnInventory, EventTriggerType.PointerClick, delegate { OnClick(btnInventory); });

        //button Back
        AddEvent(btnBack, EventTriggerType.PointerEnter, delegate { OnEnter(btnBack); });
        AddEvent(btnBack, EventTriggerType.PointerClick, delegate { OnClick(btnBack); });

        //button Confirm
        AddEvent(btnConfirm, EventTriggerType.PointerEnter, delegate { OnEnter(btnConfirm); });
        AddEvent(btnConfirm, EventTriggerType.PointerClick, delegate { OnClick(btnConfirm); });

        //button EnterStage
        AddEvent(btnDepart, EventTriggerType.PointerEnter, delegate { OnEnter(btnDepart); });
        AddEvent(btnDepart, EventTriggerType.PointerClick, delegate { OnClick(btnDepart); });

    }

    public void OnEnter(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonHover");
    }

    public void OnClick(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("buttonPressed");
    }

    public Vector3 GetPlayerDelta()
    {
        var playerPosDelta = new Vector3();
        /*playerPosDelta = playerSprite.GetComponent<Vector3>() - activeStage.GetComponent<Vector3>();*/
        playerPosDelta = playerSprite.transform.position - activeStage.transform.position;
        return playerPosDelta;
    }

    /*public void MovePlayerPosition(GameObject nextStage)
    {
        playerSprite.transform.position = GetPlayerDelta() + nextStage.transform.position;
        GetPlayerDelta();
    }*/

    public void MovePlayerPosition(GameObject nextStage)
    {
        // playerSprite.transform.position = GetPlayerDelta() + nextStage.transform.position;
        // GetPlayerDelta();
        if (nextStage.name == "Stage 1") animator.Play("Go To Stage");
        else if (nextStage.name == "Stage Zero") animator.Play("Go To Home");
    }

    /*public IEnumerator MovePlayerPosition(GameObject nextStage)
    {
        var playerNextPos = GetPlayerDelta() + nextStage.transform.position;

        while (Vector3.Distance(playerSprite.transform.position, playerNextPos) > 0.1f)
        {
            playerSprite.transform.position = Vector3.MoveTowards(playerSprite.transform.position, playerNextPos, speed * Time.deltaTime);
            yield return null; // Tunggu frame berikutnya sebelum melanjutkan loop
        }
        playerSprite.transform.position = GetPlayerDelta() + nextStage.transform.position;

        // Set posisi akhir secara eksplisit untuk memastikan posisi akhir tepat
        playerSprite.transform.position = playerNextPos;

        GetPlayerDelta();
    }*/

    public void BackToMenu()
    {
        SceneTransition.instance.LoadScene("MainMenu");
    }

    public bool EnterStage()
    {
        return true;
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
