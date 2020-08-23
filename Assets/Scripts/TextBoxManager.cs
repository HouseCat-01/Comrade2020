using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;

<<<<<<< HEAD
    private float typeSpeed = 2.5f;
=======
    public float typeSpeed;
>>>>>>> parent of 7fbe270... Text scroll basic implimentation

    // Start is called before the first frame update
    void Start()
    {
        if(textFile != null)
        { 
            textLines = (textFile.text.Split('\n'));
        }
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
<<<<<<< HEAD
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
=======
        theText.text = textLines[currentLine];
>>>>>>> parent of 7fbe270... Text scroll basic implimentation

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
        }
<<<<<<< HEAD
=======

       
>>>>>>> parent of 7fbe270... Text scroll basic implimentation
    }

    private IEnumerator TextScroll (string lineofText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
<<<<<<< HEAD
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
=======
        while(isTyping  && !cancelTyping)
>>>>>>> parent of 7fbe270... Text scroll basic implimentation
    }
    private IEnumerator PauseScroll() 
    {
        isTyping = true;
        yield return new WaitForSeconds(2.0f);
        isTyping = false;
    }
}
