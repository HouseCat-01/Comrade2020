using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierTracker : MonoBehaviour
{
    public static string[,] conflicts = { {"Food Surplus", "Food Shortage"}, { "Housing Shortage", "Major Housing Shortage" } } ;
    public static List<Modifier> startingModifiers = new List<Modifier>();
    public static List<Modifier> possibleModifiers = new List<Modifier>();
    // Start is called before the first frame update
    void Start() {
        Modifier temp = new Modifier("Housing Shortage", "unrest=+2");
        startingModifiers.Add(temp);
        temp = new Modifier("Rural Unrest", "unrest=+4;foodIncome=-1");
        startingModifiers.Add(temp);
        temp = new Modifier("Grain Requisitions", "unrestChange=+0.1;foodIncome=+2");
        startingModifiers.Add(temp);


        temp = new Modifier("Food Shortage", "unrestChange=+0.2", "food=<1");
        possibleModifiers.Add(temp);
        temp = new Modifier("Food Surplus", "unrestChange=-0.1", "food=>10");
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
    
}
