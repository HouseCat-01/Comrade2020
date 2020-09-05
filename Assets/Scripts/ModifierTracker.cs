using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierTracker : MonoBehaviour
{
    public static string[,] conflicts = { {"Food Surplus", "Food Shortage"}, { "Housing Shortage", "Major Housing Shortage" } } ;
    public static string[] startingModsList = { "Rural Unrest", "Grain Requisitions" };
    public static string[] possibleModsList = { "Food Shortage", "Food Surplus" };
    public static List<Modifier> modifierList = new List<Modifier>();
    public static List<Modifier> startingModifiers = new List<Modifier>();
    public static List<Modifier> possibleModifiers = new List<Modifier>();
    // Start is called before the first frame update
    void Start() {
        Modifier temp = new Modifier("Housing Shortage", "unrest=+2");
        modifierList.Add(temp);
        temp = new Modifier("Rural Unrest", "unrest=+4;foodIncome=-1");
        modifierList.Add(temp);
        temp = new Modifier("Grain Requisitions", "unrestChange=+0.1;foodIncome=+2");
        modifierList.Add(temp);
        temp = new Modifier("Food Shortage", "unrestChange=+0.2", "food=<1");
        modifierList.Add(temp);
        temp = new Modifier("Food Surplus", "unrestChange=-0.1", "food=>10");
        startingModifiers = GetModifiers(startingModsList);
        possibleModifiers = GetModifiers(possibleModsList);
    }
    public static void CheckPossible() {
        foreach (Modifier modifier in possibleModifiers) {
            if (modifier.CheckRequirements() && CheckConflicts(modifier)) {
                ResourceManager.AddModifier(modifier);
            }
        }
    }
    public static bool CheckConflicts(Modifier newModifier) {
        foreach (Modifier modifier in ResourceManager.modifiers) {
            if (modifier.name == newModifier.name) {
                return false;
            }
        }
        return true;
    }
    public static void RemoveConflicts(Modifier newModifier) {
        if (newModifier.name == "Major Housing Shortage") {
            foreach (Modifier modifier in ResourceManager.modifiers) {
                if (modifier.name == "Housing Shortage") {
                    ResourceManager.modifiers.Remove(modifier);
                }
            }
        }
    }

    public static Modifier getModifier(string name) {
        for(int i = 0; i < modifierList.Count; i++) {
            if(modifierList[i].name == name) {
                return modifierList[i];
            }
        }
        return null;
    }
    public static List<Modifier> GetModifiers(string[] names) {
        List<Modifier> modifiers = new List<Modifier>();
        for (int j = 0; j < names.Length; j++) {
            for (int i = 0; i < modifierList.Count; i++) {
                if (modifierList[i].name == names[j].Trim()) {
                    modifiers.Add(modifierList[i]);
                    break;
                }
            }
        }
        return modifiers;
    }
}
