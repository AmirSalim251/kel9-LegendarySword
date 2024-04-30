using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI sentence;
    public string[] lines;
    public float textSpeed = 0.05f;

    public TextAsset textFile;

    List<string> speakers = new List<string>();
    List<string> sentences = new List<string>();
    
    private int index = 0;

    void Start() 
    {
        speaker.text = string.Empty;
        sentence.text = string.Empty;

        ReadFile();
        StartCoroutine(TypeLines());
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

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (sentence.text == sentences[index]) 
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                sentence.text = sentences[index];
            }
        }
    }

    IEnumerator TypeLines() 
    {
        speaker.text = speakers[index];

        foreach (char letter in sentences[index].ToCharArray()) 
        {
            sentence.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() 
    {
        if (index < lines.Length - 1) 
        {
            index++;
            speaker.text = string.Empty;
            sentence.text = string.Empty;
            StartCoroutine(TypeLines());
        }   
        else 
        {
            gameObject.SetActive(false);
        }
    }   
}
