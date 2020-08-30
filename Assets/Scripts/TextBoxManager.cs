using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.Diagnostics;
//using System.Text.RegularExpressions;

public class TextBoxManager : MonoBehaviour
{
    
    public GameObject textBox;
    public Button buttonPrefab;

    //public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    private int currentLine;
    private int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;

    private float typeSpeed = 0.2f;

    public TextMeshProUGUI theText;
    public int totalVisibleCharacters;


    void Start()
    {
        theText.text = "";
        if(textFile != null)
        { 
            textLines = textFile.text.Split('\n');
        }
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        SetText(textLines[0]);
    }



    void Update() {
        if (!isTyping && !wait && currentLine <= endAtLine) 
        {
            string line = textLines[currentLine].Trim();
            if (line == "<wait>") {
                wait = true;
            }
            else if(line == "<pause>") {
                StartCoroutine(PauseScroll(1.5f));
            }
            else if(line == "<end>") {
                StartCoroutine(EndScroll());
            }
            else if(line == "<decision>") {
                List<Options> options = GetOptions();
                Button a = Instantiate<Button>(buttonPrefab);
                Button b = Instantiate<Button>(buttonPrefab);
                a.GetComponentInChildren<TextMeshProUGUI>().text = options[0].text;
                a.transform.SetParent(textBox.transform.parent);
                b.GetComponentInChildren<TextMeshProUGUI>().text = options[1].text;
                b.transform.SetParent(textBox.transform.parent);
            }
            else {
                StartCoroutine(TextScroll(textLines[currentLine]));
            }
            currentLine++;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            if (wait) { wait = false; }
            else if(isTyping) {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScroll(string line) {
        /*int visibleCount = counter % (totalVisibleCharacters + 1);
        theText.maxVisibleCharacters = visibleCount;
        if(visibleCount >= totalVisibleCharacters) {
            yield return new WaitForSeconds(typeSpeed);
        }
        counter += 1;*/
        SetText(line);
        isTyping = true;
        cancelTyping = false;
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
    }
    private void SetText(string text) {
        theText.text = text;
        theText.maxVisibleCharacters = 0;
        totalVisibleCharacters = theText.textInfo.characterCount;
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
    private IEnumerator PauseScroll(float time) 
    {
        isTyping = true;
        yield return new WaitForSeconds(time);
        isTyping = false;
    }
    private IEnumerator EndScroll() 
    {
        while(!Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        theText.text = "";
        textBox.SetActive(false);
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
                else if(text == "<tags>") {
                    temp.tags = textLines[++currentLine].Trim();
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
            Debug.Log(temp.text);
            list.Add(temp);
        }
        return list;
    }
}
