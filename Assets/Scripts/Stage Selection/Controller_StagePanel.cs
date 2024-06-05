using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Controller_StagePanel : MonoBehaviour
{
    public Controller_StageSelection stageController;
    public Controller_StageDetail stageDetailController;
    public StageData stageData;

    [Space]
    public TMP_Text stageName;

    // Start is called before the first frame update
    void Start()
    {
        var obj = this.gameObject;
        AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
        
        if(stageData  != null)
        {
            stageName.SetText(stageData.stageName);
        }
        
    }

    public void OnClick(GameObject obj)
    {
        /*if(stageController.activeStage == obj)
        {
            stageDetailController.OpenPanel();
        }
        else
        {
            stageController.GetPlayerDelta();
            stageController.MovePlayerPosition(obj);

            *//*StartCoroutine(stageController.MovePlayerPosition(obj));*//*

            stageController.activeStage = obj;
        }*/

        stageController.GetPlayerDelta();
        stageController.MovePlayerPosition(obj);

        //*StartCoroutine(stageController.MovePlayerPosition(obj));

        stageController.activeStage = obj;
        stageDetailController.stageData = obj.GetComponent<Controller_StagePanel>().stageData;
        Controller_GameStage.Instance.stageChosen = stageData;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            stageController.BackToMenu();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            var isStage = stageController.EnterStage();
            if(stageController.activeStage.GetComponent<Controller_StagePanel>().stageData != null)
            {
                isStage = true;
            }
            else
            {
                isStage = false;
            }

            if (isStage)
            {
                /*stageDetailController.stageData = stageData;*/
                stageDetailController.OpenPanel();
            }
        }
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
