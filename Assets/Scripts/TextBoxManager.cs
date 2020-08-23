using System.Collections;
using System.Collections.Generic;
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

    public float typeSpeed = 0.8f;

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
        if (!isTyping && currentLine <= endAtLine) 
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
            currentLine++;
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
            theText.text = theText.text + lineofText[letter];
            letter++;    
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = theText.text + '\n';
        isTyping = false;
    }
}
