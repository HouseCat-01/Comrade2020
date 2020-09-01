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

    private void Start() {
        UpdateText();
    }

    public void UpdateText() {
        unityText.text = "Unity:" + ResourceManager.GetUnity();
        trustText.text = "Trust:" + ResourceManager.GetTrust();
        unrestText.text = "Unrest:" + ResourceManager.GetUnrest();

        populationText.text = "Population:" + ResourceManager.GetPopulation();
        foodText.text = "Food:" + ResourceManager.GetFood();
        materialsText.text = "Materials:" + ResourceManager.GetMaterials();
        machineryText.text = "Machinery:" + ResourceManager.GetMachinery();
    }
}
