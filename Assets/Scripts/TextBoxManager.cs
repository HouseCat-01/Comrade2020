using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    private string[] textLines;

    private int currentLine = 0;
    private int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;

    private float typeSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        if(textFile != null)
        { 
            textLines = textFile.text.Split('\n');
        }
        //if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
        if (!isTyping && !wait && currentLine <= endAtLine) 
        {
            if (textLines[currentLine] == "<wait>") 
            {
                wait = true;
            }
            else if(textLines[currentLine] == "<pause>") {
                StartCoroutine(PauseScroll());
            }
            else 
            {
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
        }*/
        //theText.text = textLines[currentLine];

        //if(Input.GetKeyDown(KeyCode.Return))
        {
            
            /*if(!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    textBox.SetActive(false);
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }

            }

            else if(isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }*/
        }
    }

    private IEnumerator TextScroll (string lineofText)
    {
        int letter = 0;
        //theText.text = "";
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
        theText.text = theText.text + '\n';
        isTyping = false;
    }
    private IEnumerator PauseScroll() 
    {
        isTyping = true;
        yield return new WaitForSeconds(2.0f);
        isTyping = false;
    }
}
