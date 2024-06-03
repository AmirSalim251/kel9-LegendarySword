using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stage1Cutscene : MonoBehaviour
{
    public GameObject CombatUI;
    public GameObject CameraController;
    public GameObject DialogueCanvas;
    public DialogueHandler dialogueHandler;
    public GameObject CombatCamera;

    public List<CinemachineVirtualCamera> VirtualCameras;
    
    void Awake()
    {
        CombatUI.SetActive(false);
        CameraController.SetActive(false);
        CombatCamera.SetActive(false);
        StartCoroutine(Intro());
    }

    void Update()
    {
        if (dialogueHandler.isDialogueOver)
            EndCutsene();

        if (dialogueHandler.index == 4)
            VirtualCameras[2].enabled = true;
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(0.5f);
        VirtualCameras[1].enabled = true;
        yield return new WaitForSeconds(1.5f);
        DialogueCanvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        DialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }

    void EndCutsene()
    {
        CombatUI.SetActive(true);
        CameraController.SetActive(true);
        CombatCamera.SetActive(true);
        
        Destroy(gameObject, 5);
    }
}
