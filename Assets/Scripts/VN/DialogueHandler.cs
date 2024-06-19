using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogueHandler : MonoBehaviour
{
    public DialogueType dialogueType;

    public TextAsset textFile;

    [Space]
    public float changeSpeakerDelay;

    public Sprite Alex_Normal;
    public Sprite Alex_Sad;
    public Sprite Alex_Happy;
    public Sprite Freya_Normal;
    public Sprite Freya_Angry;
    public Sprite Freya_Open;
    public Sprite Magnus_Normal;
    public Sprite Magnus_Angry;
    public Sprite Magnus_Happy;
    public Sprite Sword_Normal;
    public Sprite Sword_CloseEyes;
    public Sprite Sword_Surprised;
    public Sprite Sword_Serious;
    public Sprite Sword_Confused;
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

    public List<string> speakers = new List<string>();
    public List<string> sentences = new List<string>();
    public List<string> expression = new List<string>();
    
    public int index = 0;
    private bool isNarrative;
    private bool isChangingSpeaker = false;

    void Awake()
    {
        Debug.Log("Dialog Reset");
        // Initialize variables here to ensure they are reset every time the scene is loaded
        speakers.Clear();
        sentences.Clear();
        index = 0;

        // Initialize components
        CharacterAnimator = Character.GetComponent<Animator>();
        characterImage = Character.GetComponent<Image>();
    }

    void Start()
    {
        //get Text Script
        if(Controller_GameStage.Instance != null)
        {
            if (dialogueType == DialogueType.DialogueEntry)
            {
                textFile = Controller_GameStage.Instance.stageChosen.storyStart;
            }
            else if (dialogueType == DialogueType.DialogueCombat)
            {
                textFile = Controller_GameStage.Instance.stageChosen.storyMid;
            }
            else if (dialogueType == DialogueType.DialogueEndCombat)
            {
                textFile = Controller_GameStage.Instance.stageChosen.storyEnd;
            }
        }

        /*CharacterAnimator = Character.GetComponent<Animator>();
        characterImage = Character.GetComponent<Image>();*/

        speaker.text = string.Empty;
        sentence.text = string.Empty;
        continueText.SetActive(true);

        ReadFile();

        SetCharacterImage(speakers[index], expression[index]);
        CharacterAnimator.Play("Entry");

        speaker.text = speakers[index];
        sentence.text = sentences[index];
    }


    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           if (isChangingSpeaker) ClickedWhenChangingSpeaker();
           else NextLine();
        }
    }

    void ReadFile() {
        lines = textFile.text.Split("\n\r\n");
        foreach (string line in lines)
        {
            int idx =  line.IndexOf("(");
            int idx2 = line.IndexOf(")");
            string _speaker = "";
            string _sentence = "";
            string _expression = "";
            
            isNarrative = idx == 0;

            if (isNarrative) _sentence = line;
            else {
                _speaker = line.Substring(0, idx);
                _expression = line.Substring(idx+1, idx2-idx-1);
                _sentence = line.Substring(idx2+3);
            }

            speakers.Add(_speaker);
            sentences.Add(_sentence);
            expression.Add(_expression);
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
            SetCharacterImage(speakers[index], expression[index]);
        }
    }   

    void SetCharacterImage(string speaker, string expression)
    {
        characterImage.enabled = true;

        if (speaker == "Alex") 
        {
            if (expression == "Normal") characterImage.sprite = Alex_Normal;
            else if (expression == "Sad") characterImage.sprite = Alex_Sad;
            else if (expression == "Happy") characterImage.sprite = Alex_Happy;
        }

        else if (speaker == "Freya") 
        {
            if (expression == "Normal") characterImage.sprite = Freya_Normal;
            else if (expression == "Angry") characterImage.sprite = Freya_Angry;
            else if (expression == "Open") characterImage.sprite = Freya_Open;
        }

        else if (speaker == "Magnus") 
        {
            if (expression == "Normal") characterImage.sprite = Magnus_Normal;
            else if (expression == "Angry") characterImage.sprite = Magnus_Angry;
            else if (expression == "Happy") characterImage.sprite = Magnus_Happy;
        }

        else if (speaker == "Sword") 
        {
            if (expression == "Normal") characterImage.sprite = Sword_Normal;
            else if (expression == "CloseEyes") characterImage.sprite = Sword_CloseEyes;
            else if (expression == "Surprised") characterImage.sprite = Sword_Surprised;
            else if (expression == "Confused") characterImage.sprite = Sword_Confused;
            else if (expression == "Serious") characterImage.sprite = Sword_Serious;
        }

        else if (speaker == "Werewolf") characterImage.sprite = Werewolf;

        else characterImage.enabled = false;
    }

    IEnumerator ChangeSpeaker()
    {
        isChangingSpeaker = true;
        ExitAnimation();

        continueText.SetActive(false);

        yield return new WaitForSeconds(changeSpeakerDelay);

        EntryAnimation();
        continueText.SetActive(true);   
        isChangingSpeaker = false;
    }

   
    void ClickedWhenChangingSpeaker()
    {
        StopAllCoroutines();
        isChangingSpeaker = false;
        EntryAnimation();
        continueText.SetActive(true);
    }

    void EntryAnimation()
    {
        isNarrative = speakers[index] == "";

        if (!isNarrative) 
        {
            speaker.text = speakers[index];
            sentence.text = sentences[index];
            SetCharacterImage(speakers[index], expression[index]);

            if (speakers[index] == "Werewolf") CharacterAnimator.Play("Werewolf Entry");
            else if (speakers[index] == "Sword") CharacterAnimator.Play("Sword Entry");
            else CharacterAnimator.Play("Entry");

            DialogueAnimator.Play("Entry");
        }
        else 
        {
            narrative.text = sentences[index];
            DialogueAnimator.Play("Narrative Entry"); 
        }
    }

    void ExitAnimation()
    {
        if (speakers[index-1] == "Werewolf") CharacterAnimator.Play("Werewolf Exit");
        else if (speakers[index-1] == "Sword") CharacterAnimator.Play("Sword Exit");
        else if (speakers[index-1] != "") CharacterAnimator.Play("Exit");

        if (speakers[index-1] != "") DialogueAnimator.Play("Exit");
        else DialogueAnimator.Play("Narrative Exit");
    }
    
    IEnumerator EndDialogue()
    {
        continueText.SetActive(false);
        ExitAnimation();

        yield return new WaitForSeconds(changeSpeakerDelay);

        isDialogueOver = true;

        if(SceneManager.GetActiveScene().name == "VN Scene") 
            SceneTransition.instance.LoadScene("CombatScene 3");
    }

}

public enum DialogueType
{
    DialogueEntry,
    DialogueCombat,
    DialogueEndCombat
}

