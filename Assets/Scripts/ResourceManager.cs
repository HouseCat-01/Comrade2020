using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    //UI interface
    public static UIManager uiTracker;

    //tags
    public static List<Modifier> modifiers = new List<Modifier>();

    //date
    private static int turn = 1;
    private static int month = 9;
    private static int year = 1920;

    //basic stats
    private static int unity = 100;
    private static int trust = 100;
    private static int unrest = 0;

    private static int unrestChange = 0;

    //resources
    private static int population = 30000;
    private static int food = 10;
    private static int materials = 4;
    private static int machinery = 0;

    private static int foodIncome = 0;
    private static int materialsIncome = 0;
    private static int machineryIncome = 0;

    private static bool delegated = false;

    // Start is called before the first frame update
    void Start()
    {
        uiTracker = gameObject.GetComponent<UIManager>();
    }

    public static void AddUnity(int num) {
        SetUnity(num + unity);
    }
    public static void SetUnity(int num) {
        unity = num;
        DelegateUpdate();
    }
    public static int GetUnity() {
        return unity;
    }
    public static void AddTrust(int num) {
        SetTrust(num + trust);
    }
    public static void SetTrust(int num) {
        trust = num;
        DelegateUpdate();
    }
    public static int GetTrust() {
        return trust;
    }
    public static void AddUnrest(int num) {
        SetUnrest(num + trust);
    }
    public static void SetUnrest(int num) {
        if (num < 0) { num = 0; }
        unrest = num;
        DelegateUpdate();
    }
    public static int GetUnrest() {
        return unrest;
    }
    public static void AddUnrestChange(int num) {
        SetUnrestChange(num + unrestChange);
    }
    public static void SetUnrestChange(int num) {
        if (num < 0) { num = 0; }
        unrestChange = num;
        DelegateUpdate();
    }
    public static int GetUnrestChange() {
        return unrestChange;
    }

    public static void AddPopulation(int pop) {
        SetPopulation(pop + population);
    }
    public static void SetPopulation(int pop) {
        if (pop < 0) { pop = 0; }
        population = pop;
        DelegateUpdate();
    }
    public static int GetPopulation() {
        return population;
    }
    public static void AddFood(int num) {
        SetFood(num + food);
    }
    public static void SetFood(int num) {
        if (num < 0) { num = 0; }
        food = num;
        DelegateUpdate();
    }
    public static int GetFood() {
        return food;
    }
    public static void AddMaterials(int num) {
        SetMaterials(num + materials);
    }
    public static void SetMaterials(int num) {
        if (num < 0) { num = 0; }
        materials = num;
        DelegateUpdate();
    }
    public static int GetMaterials() {
        return materials;
    }
    public static void AddMachinery(int num) {
        SetMachinery(num + machinery);
    }
    public static void SetMachinery(int num) {
        if(num<0) { num = 0; }
        machinery = num;
        DelegateUpdate();
    }
    public static int GetMachinery() {
        return machinery;
    }

    public static int GetFoodIncome() {
        return foodIncome;
    }
    public static void AddFoodIncome(int num) {
        SetFoodIncome(num + foodIncome);
    }
    public static void SetFoodIncome(int num) {
        foodIncome = num;
        DelegateUpdate();
    }

    public static int GetMaterialsIncome() {
        return materialsIncome;
    }
    public static void AddMaterialsIncome(int num) {
        SetMaterialsIncome(num + materialsIncome);
    }
    public static void SetMaterialsIncome(int num) {
        materialsIncome = num;
        DelegateUpdate();
    }

    public static int GetMachineryIncome() {
        return machineryIncome;
    }
    public static void AddMachineryIncome(int num) {
        SetMachineryIncome(num + machineryIncome);
    }
    public static void SetMachineryIncome(int num) {
        machineryIncome = num;
        DelegateUpdate();
    }

    public static void DelegateUpdate() {
        if(!uiTracker) {
            delegated = true;
        }
        else {
            uiTracker.UpdateText();
            delegated = false;
        }
    }

    public int GetMonth() {
        return month;
    }
    public int GetYear() {
        return year;
    }
    public static void NextTurn() {
        if (month == 12) {
            month = 1;
            year++;
        }
        else {
            month++;
        }
        AddFood(foodIncome);
        AddMaterials(materialsIncome);
        AddMachinery(machineryIncome);
        AddUnrest(unrestChange);
        foreach(Modifier modifier in modifiers) {
            if(!modifier.CheckRequirements()) {
                modifier.OnRemove();
                modifiers.Remove(modifier);
            }
        }
    }

    public static void AddModifier(Modifier modifier) {
        if(!HasModifier(modifier)) {
            modifiers.Add(modifier);
            modifier.OnAdd();
        }
    }

    public static bool HasModifier(Modifier other) {
        foreach(Modifier modifier in modifiers) {
            if(modifier.name == other.name) {
                return true;
            }
        }
        return false;
    }

    /*public enum Tag
    {
        embraced_nep, rejected_nep, peasant_revolt, green_army
    }*/
}
