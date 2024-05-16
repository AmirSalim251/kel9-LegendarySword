using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Controller_StagePanel : MonoBehaviour
{
    public Controller_StageSelection stageController;
    public Controller_StageDetail stageDetailController;
    // Start is called before the first frame update
    void Start()
    {
        var obj = this.gameObject;
        AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
    }

    public void OnClick(GameObject obj)
    {
        if(stageController.activeStage == obj)
        {
            stageDetailController.OpenPanel();
        }
        else
        {
            stageController.GetPlayerDelta();
            stageController.MovePlayerPosition(obj);
            stageController.activeStage = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
