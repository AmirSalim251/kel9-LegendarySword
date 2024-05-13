using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public TextAsset textFile;
    public float changeSpeakerDelay;

    public TextMeshProUGUI speaker;
    public TextMeshProUGUI sentence;

    public Animator dialogue;

    public GameObject continueText;

    public Animator Alex;
    public Animator Freya;
    public Animator Magnus;

    public string[] lines;

    List<string> speakers = new List<string>();
    List<string> sentences = new List<string>();
    
    private int index = 0;
    private bool changingSpeaker = false;

    void Start() 
    {
        speaker.text = string.Empty;
        sentence.text = string.Empty;
        continueText.SetActive(true);

        ReadFile();

        SetCharacterImage(speakers[index], true);

        speaker.text = speakers[index];
        sentence.text = sentences[index];
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           if (changingSpeaker) ClickedWhenChangingSpeaker();
           else NextLine();
        }
    }

    void ReadFile() {
        lines = textFile.text.Split("\n\r\n");
        foreach (string line in lines)
        {
            int idx =  line.IndexOf(":");
            string _speaker;
            string _sentence;
            
            if (idx != -1)  _speaker = line.Substring(0, idx);
            else 
            {
                _speaker = "";
                idx = -2;
            }

            _sentence = line.Substring(idx + 2);

            speakers.Add(_speaker);
            sentences.Add(_sentence);
        }
    }

    void NextLine() 
    {        
        index++;

        if (index >= lines.Length) 
        {
            if (index == lines.Length) StartCoroutine(EndDialogue());
            return;
        }

        if (speakers[index] != speakers[index-1]) StartCoroutine(ChangeSpeaker());
        else 
        {
            dialogue.SetTrigger("Next Line");
            speaker.text = speakers[index];
            sentence.text = sentences[index];
        }
    }   

    void SetCharacterImage(string speaker, bool activate)
    {
        if (speaker == "Alex") 
        {
            if (activate == true) Alex.SetTrigger("Entry");
            else Alex.SetTrigger("Exit");
        }
        else if (speaker == "Freya")
        {
            if (activate == true) Freya.SetTrigger("Entry");
            else Freya.SetTrigger("Exit");
        }
        else if (speaker == "Magnus")
        {
            if (activate == true) Magnus.SetTrigger("Entry");
            else Magnus.SetTrigger("Exit");
        }
    }

    IEnumerator ChangeSpeaker()
    {
        changingSpeaker = true;

        SetCharacterImage(speakers[index-1], false);
        dialogue.SetTrigger("Exit");
        continueText.SetActive(false);

        yield return new WaitForSeconds(changeSpeakerDelay);

        speaker.text = speakers[index];
        sentence.text = sentences[index];

        SetCharacterImage(speakers[index], true);
        dialogue.SetTrigger("Entry");
        continueText.SetActive(true);

        changingSpeaker = false;
    }

    IEnumerator EndDialogue()
    {
        SetCharacterImage(speakers[index-1], false);
        dialogue.SetTrigger("Exit");

        yield return new WaitForSeconds(changeSpeakerDelay);

        Destroy(gameObject);
    }

    void ClickedWhenChangingSpeaker()
    {
        StopAllCoroutines();
        changingSpeaker = false;

        speaker.text = speakers[index];
        sentence.text = sentences[index];

        SetCharacterImage(speakers[index], true);
        dialogue.SetTrigger("Entry");
        continueText.SetActive(true);
    }
}
