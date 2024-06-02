using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueHandler : MonoBehaviour
{
    public TextAsset textFile;
    public float changeSpeakerDelay;

    public Sprite Alex;
    public Sprite Freya;
    public Sprite Magnus;
    public Sprite Werewolf;

    public GameObject Character;

    public TextMeshProUGUI speaker;
    public TextMeshProUGUI sentence;
    public TextMeshProUGUI narrative;

    public Animator DialogueAnimator;
    
    public GameObject continueText;

    private Animator CharacterAnimator;
    private Image characterImage;

    public string[] lines;

    public bool isDialogueOver = false;

    List<string> speakers = new List<string>();
    List<string> sentences = new List<string>();
    
    public int index = 0;
    private bool changingSpeaker = false;

    void Start() 
    {
        CharacterAnimator = Character.GetComponent<Animator>();
        characterImage = Character.GetComponent<Image>();

        speaker.text = string.Empty;
        sentence.text = string.Empty;
        continueText.SetActive(true);

        ReadFile();

        SetCharacterImage(speakers[index]);
        CharacterAnimator.Play("Entry");

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
            DialogueAnimator.Play("Next Line");
            speaker.text = speakers[index];
            sentence.text = sentences[index];
        }
    }   

    void SetCharacterImage(string speaker)
    {
        characterImage.enabled = true;

        if (speaker == "Alex") characterImage.sprite = Alex;

        else if (speaker == "Freya") characterImage.sprite = Freya;

        else if (speaker == "Magnus") characterImage.sprite = Magnus;

        else if (speaker == "Werewolf") characterImage.sprite = Werewolf;

        else characterImage.enabled = false;
    }

    IEnumerator ChangeSpeaker()
    {
        changingSpeaker = true;

        if (speakers[index-1] != "Werewolf") CharacterAnimator.Play("Exit");
        else CharacterAnimator.Play("Werewolf Exit");

        if (index > 0 && speakers[index-1] != "Narrative") DialogueAnimator.Play("Exit");
        else DialogueAnimator.Play("Narrative Exit");

        continueText.SetActive(false);

        yield return new WaitForSeconds(changeSpeakerDelay);

        if (speakers[index] != "Narrative") 
        {
            speaker.text = speakers[index];
            sentence.text = sentences[index];
            SetCharacterImage(speakers[index]);

            if (speakers[index] != "Werewolf") CharacterAnimator.Play("Entry");
            else CharacterAnimator.Play("Werewolf Entry");

            DialogueAnimator.Play("Entry");
        }
        else 
        {
            narrative.text = sentences[index];
            DialogueAnimator.Play("Narrative Entry"); 
        }
        
        continueText.SetActive(true);   

        changingSpeaker = false;
    }

    void ClickedWhenChangingSpeaker()
    {
        StopAllCoroutines();
        changingSpeaker = false;

        if (speakers[index] != "Narrative")
        {
            speaker.text = speakers[index];
            sentence.text = sentences[index];
            SetCharacterImage(speakers[index]);

            if (speakers[index] != "Werewolf") CharacterAnimator.Play("Entry");
            else CharacterAnimator.Play("Werewolf Entry");

            DialogueAnimator.Play("Entry");

            continueText.SetActive(true);
        }
        else
        {
            narrative.text = sentences[index];
            DialogueAnimator.Play("Narrative Entry");
        }
    }

    IEnumerator EndDialogue()
    {
        continueText.SetActive(false);

        CharacterAnimator.Play("Exit");
        DialogueAnimator.Play("Exit");

        yield return new WaitForSeconds(changeSpeakerDelay);

        isDialogueOver = true;

        if(SceneManager.GetActiveScene().name == "VN Scene") 
            SceneManager.LoadScene("CombatScene 3");
    }
}
