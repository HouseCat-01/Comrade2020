using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//random change
public class TextImporter : MonoBehaviour
{
    public TextAsset textFile;
    public string[] textLines;

    // Start is called before the first frame update
    void Start()
    {
     if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));

        }
    }
}
