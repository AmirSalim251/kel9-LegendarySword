using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public GameObject dialogue;

    void Start()
    {
        StartCoroutine(ActivateDialogue());
    }

    IEnumerator ActivateDialogue()
    {
        yield return new WaitForSeconds(1.5f);
        dialogue.SetActive(true);
    }
}
