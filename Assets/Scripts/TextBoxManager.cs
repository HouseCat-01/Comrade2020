﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using System.Diagnostics;
//using System.Text.RegularExpressions;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public Button buttonPrefab;

    //public Text theText;

    public TextAsset[] textFiles = new TextAsset[12];
    private static int currentFile = 0;

    public string[] textLines;

    private int currentLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;
    //private bool decision = false;

    private float typeSpeed = 0.1f;

    public TextMeshProUGUI theText;
    public TextMeshProUGUI theSpeaker;
    public int totalVisibleCharacters;


    void Start()
    {
        theText.text = "";
        if(textFiles[currentFile] != null)
        { 
            textLines = textFiles[currentFile].text.Split('\n');
        }
        Process();
    }



    void Update() {
        /*if (!isTyping && !wait && !decision && currentLine <= endAtLine) {
            Process();   
        }*/
        if (Input.GetMouseButtonDown(0)) {
            if (wait) { wait = false; }
            else if (isTyping) {
                cancelTyping = true;
            }
        }
    }
    private void Process() {
        if (currentLine >= textLines.Length) {
            EndScroll();
            return;
        }
        string line = textLines[currentLine].Trim();
        if (line == "<wait>") {
            wait = true;
        }
        else if (line == "<pause>") {
            StartCoroutine(PauseScroll(1.5f));
        }
        else if (line == "<end>") {
            EndScroll();
        }
        else if (line == "<goto>") {
            string search = textLines[++currentLine].Trim();
            for (int i = currentLine + 1; i < textLines.Length; i++) {
                if (search == textLines[i].Trim()) {
                    currentLine = i + 1;
                    Process();
                    return;
                }
            }
        }
        else if(line == "<speaker>") {
            theSpeaker.text = textLines[++currentLine];
            currentLine++;
            Process();
        }
        else if (line == "<decision>") {
            List<Options> options = GetOptions();
            List<Button> buttons = new List<Button>();
            RectTransform parent = textBox.transform.parent.gameObject.GetComponent<RectTransform>();
            float min = 0;
            float delta = 0.5f / options.Count;
            for (int i = 0; i < options.Count; i++) {
                Button button = Instantiate(buttonPrefab);
                button.transform.SetParent(textBox.transform.parent);
                RectTransform transform = button.GetComponent<RectTransform>();
                transform.anchorMin = new Vector2(0, min);
                transform.anchorMax = new Vector2(1, min += delta);
                transform.offsetMin = new Vector2(0, 0);
                transform.offsetMax = new Vector2(0, 0);
                button.GetComponentInChildren<TextMeshProUGUI>().text = options[i].text;
                int temp = i;
                button.onClick.AddListener(() => DecisionClick(buttons, options[temp]));
                buttons.Add(button);
            }
            //decision = true;
        }
        else {
            StartCoroutine(TextScroll(textLines[currentLine]));
        }
    }

    private void DecisionClick(List<Button> buttons, Options option) {
        //Debug.Log(options[index].text);
        //Debug.Log(buttons[index].GetComponent<Text>().text);
        //Debug.Log("buttons: " + buttons.Count + ", options: " + options.Count + ", index: " + index);
        //decision = false;
        for (int i = 0; i < buttons.Count; i++) {
            Destroy(buttons[i].gameObject);
        }
        option.ParseEffects();
        if (option.modifiers.Count > 0) {
            ResourceManager.AddModifiers(option.modifiers);
        }
        if (option.next != "") {
            string search = option.next.Trim();
            for (int i = currentLine + 1; i < textLines.Length; i++) {
                if (search == textLines[i].Trim()) {
                    currentLine = i + 1;
                    Process();
                    return;
                }
            }
        }
        else {
            currentLine++;
        }
        Process();
    }

    private IEnumerator TextScroll(string line) {
        SetText(line);
        isTyping = true;
        cancelTyping = false;
        totalVisibleCharacters = 0;
        while (isTyping && !cancelTyping && theText.maxVisibleCharacters <= theText.textInfo.characterCount) {
            totalVisibleCharacters += 1;
            theText.maxVisibleCharacters = totalVisibleCharacters;
            yield return new WaitForSeconds(typeSpeed);
        }
        if (cancelTyping) {
            theText.maxVisibleCharacters = theText.textInfo.characterCount;
            cancelTyping = false;
        }
        theText.text += '\n';
        isTyping = false;
        currentLine++;
        Process();
    }
    private void SetText(string text) {
        theText.text = text;
        theText.maxVisibleCharacters = 0;
        totalVisibleCharacters = theText.textInfo.characterCount;
    }

    private IEnumerator PauseScroll(float time) 
    {
        isTyping = true;
        yield return new WaitForSeconds(time);
        isTyping = false;
        currentLine++;
        Process();
    }
    private void EndScroll() 
    {
        Button a = Instantiate<Button>(buttonPrefab);
        a.GetComponentInChildren<TextMeshProUGUI>().text = "End Dialogue";
        a.transform.SetParent(textBox.transform.parent);
        a.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        a.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        //a.transform.localPosition = new Vector2(0, 100);

        //decision = true;
        currentFile++;

        a.onClick.AddListener(() => EndDialogue());
    }

    private void EndDialogue() {
        SceneManager.LoadSceneAsync(1);
    }

    private List<Options> GetOptions() 
    {
        currentLine++;
        string text = textLines[currentLine].Trim();
        List<Options> list = new List<Options>();
        while(text != "</decision>") {
            Options temp = new Options();
            text = textLines[++currentLine].Trim();
            while (text != "<option>" && text != "</decision>") {
                if (text == "<text>") {
                    temp.text = textLines[++currentLine].Trim();
                }
                else if(text == "<effects>") {
                    temp.effects = textLines[++currentLine].Trim();
                }
                else if(text == "<modifiers>") {
                    temp.modifiers = ModifierTracker.GetModifiers(textLines[++currentLine].Trim().Split(';'));
                }
                else if(text == "<results>") {
                    temp.results = textLines[++currentLine].Trim();
                }
                else if (text == "<requirements>") {
                    temp.requirements = textLines[++currentLine].Trim();
                }
                else if (text == "<next>") { 
                    temp.next = textLines[++currentLine].Trim();
                }
                text = textLines[++currentLine].Trim();
            }
            list.Add(temp);
        }
        return list;
    }
    /*private IEnumerator TextScroll (string lineofText)
{
    int letter = 0;
    isTyping = true;
    cancelTyping = false;
    while (isTyping && !cancelTyping) 
    {
        if (letter >= lineofText.Length) {
            break;
        }
        theText.text += lineofText[letter];
        letter++;    
        yield return new WaitForSeconds(typeSpeed);
    }
    if(cancelTyping) {
        theText.text += lineofText.Substring(letter);
        cancelTyping = false;
    }
    theText.text += '\n';
    isTyping = false;
}*/
}
