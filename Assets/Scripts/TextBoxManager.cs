using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//using System.Text.RegularExpressions;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    private int currentLine;
    private int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;

    private float typeSpeed = 0.05f;

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
    }

    void Update()
    {
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
                GetOptions();
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
        /*if (currentLine > endAtLine) {
            textBox.SetActive(false);
        }
        //theText.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    textBox.SetActive(false);
                }
                else
                {
                    StartCoroutine(TextScroll(textLine[currentLine]));
                }

            }

            else if(isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }*/
    }

    private IEnumerator TextScroll (string lineofText)
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
    }
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
