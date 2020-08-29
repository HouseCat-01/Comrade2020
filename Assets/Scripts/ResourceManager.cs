using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    //tags
    List<Tag> tags = new List<Tag>();

    //overall stats
    public Text unityText;
    public Text trustText;
    public Text unrestText;
    private int unity;
    private int trust;
    private int unrest;

    //resources
    public Text populationText;
    public Text foodText;
    public Text materialsText;
    public Text machineryText;
    private int population;
    private int food;
    private int materials;
    private int machinery;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddUnity(int num) {
        SetUnity(num + unity);
    }
    public void SetUnity(int num) {
        unity = num;
        unityText.text = "Unity:" + unity;
    }
    public int GetUnity() {
        return unity;
    }
    public void AddTrust(int num) {
        SetTrust(num + trust);
    }
    public void SetTrust(int num) {
        trust = num;
        trustText.text = "Trust:" + trust;
    }
    public int GetTrust() {
        return trust;
    }
    public void AddUnrest(int num) {
        SetUnrest(num + trust);
    }
    public void SetUnrest(int num) {
        unity = num;
        unrestText.text = "Unrest:" + population;
    }
    public int GetUnrest() {
        return unrest;
    }


    public void AddPopulation(int pop) {
        SetPopulation(pop + population);
    }
    public void SetPopulation(int pop) {
        population = pop;
        populationText.text = "Population:" + population;
    }
    public int GetPopulation() {
        return population;
    }
    public void AddFood(int num) {
        SetFood(num + food);
    }
    public void SetFood(int num) {
        food = num;
        foodText.text = "Food:" + food;
    }
    public int GetFood() {
        return food;
    }
    public void AddMaterials(int num) {
        SetMaterials(num + materials);
    }
    public void SetMaterials(int num) {
        materials = num;
        materialsText.text = "Materials:" + materials;
    }
    public int GetMaterials() {
        return materials;
    }
    public void AddMachinery(int num) {
        SetMachinery(num + machinery);
    }
    public void SetMachinery(int num) {
        machinery = num;
        machineryText.text = "Machinery:" + machinery;
    }
    public int GetMachinery() {
        return machinery;
    }

    public enum Tag
    {
        embraced_nep, rejected_nep, peasant_revolt, green_army
    }
}
