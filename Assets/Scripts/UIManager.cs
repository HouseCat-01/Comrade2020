using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text unityText;
    public Text trustText;
    public Text unrestText;

    public Text populationText;
    public Text foodText;
    public Text materialsText;
    public Text machineryText;

    public void UpdateText() {
        unityText.text = "Unity:" + ResourceManager.GetUnity();
        trustText.text = "Trust:" + ResourceManager.GetUnity();
        unrestText.text = "Unrest:" + ResourceManager.GetUnity();

        populationText.text = "Population:" + ResourceManager.GetUnity();
        foodText.text = "Food:" + ResourceManager.GetUnity();
        materialsText.text = "Materials:" + ResourceManager.GetUnity();
        machineryText.text = "Machinery:" + ResourceManager.GetUnity();
    }
}
