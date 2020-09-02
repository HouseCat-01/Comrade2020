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
    //public static List<Tag> tags = new List<Tag>();

    //basic stats
    private static int unity = 100;
    private static int trust = 100;
    private static int unrest = 0;

    //resources
    private static int population = 30000;
    private static int food = 10;
    private static int materials = 4;
    private static int machinery = 0;

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
        unrest = num;
        DelegateUpdate();
    }
    public static int GetUnrest() {
        return unrest;
    }


    public static void AddPopulation(int pop) {
        SetPopulation(pop + population);
    }
    public static void SetPopulation(int pop) {
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
        machinery = num;
        DelegateUpdate();
    }
    public static int GetMachinery() {
        return machinery;
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

    /*public enum Tag
    {
        embraced_nep, rejected_nep, peasant_revolt, green_army
    }*/
}
