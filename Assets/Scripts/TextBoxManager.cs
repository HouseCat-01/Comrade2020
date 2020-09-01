using System.Collections;
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

    public TextAsset textFile;
    public string[] textLines;

    private int currentLine;
    private int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool wait = false;
    private bool decision = false;

    private float typeSpeed = 0.1f;

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
        Process();
    }



    void Update() {
        /*if (!isTyping && !wait && !decision && currentLine <= endAtLine) {
            Process();   
        }*/
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (wait) { wait = false; }
            else if (isTyping) {
                cancelTyping = true;
            }
        }
    }
    private void Process() {
        if(currentLine > endAtLine) {
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
        else if (line == "<decision>") {
            List<Options> options = GetOptions();
            Button a = Instantiate<Button>(buttonPrefab);
            Button b = Instantiate<Button>(buttonPrefab);
            a.GetComponentInChildren<TextMeshProUGUI>().text = options[0].text;
            a.transform.SetParent(textBox.transform.parent);
            
            b.GetComponentInChildren<TextMeshProUGUI>().text = options[1].text;
            b.transform.SetParent(textBox.transform.parent);

            a.transform.localPosition = new Vector2(0, 100);
            b.transform.localPosition = new Vector2(0, -50);

            decision = true;

            a.onClick.AddListener(() => DecisionClick(a, options[0]));
            b.onClick.AddListener(() => DecisionClick(b, options[1]));
        }
        else {
            StartCoroutine(TextScroll(textLines[currentLine]));
        }
    }

    private void DecisionClick(Button button, Options option) {
        decision = false;
        Transform parent = button.transform.parent;
        for(int i = parent.childCount-1; i >= 0; i--) {
            if (parent.GetChild(i).GetComponent<Button>() is Button) {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
        currentLine++;
        Process();
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
        /*while(!Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        theText.text = "";
        textBox.SetActive(false);
        */
        Button a = Instantiate<Button>(buttonPrefab);
        a.GetComponentInChildren<TextMeshProUGUI>().text = "End Dialogue";
        a.transform.SetParent(textBox.transform.parent);

        a.transform.localPosition = new Vector2(0, 100);

        decision = true;

        a.onClick.AddListener(() => EndDialogue());
    }

    private void EndDialogue() {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(2);
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
