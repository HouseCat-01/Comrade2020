using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierTracker : MonoBehaviour
{
    public static List<Modifier> startingModifiers;
    public static List<Modifier> possibleModifiers;
    // Start is called before the first frame update
    void Start() {
        Modifier temp = new Modifier("Housing Shortage", "unrest=+2");
        startingModifiers.Add(temp);
        temp = new Modifier("Rural Unrest", "unrest=+4");
        startingModifiers.Add(temp);
    }
    public static void CheckPossible() {
        foreach (Modifier modifier in possibleModifiers) {
            if (modifier.CheckRequirements()) {
                ResourceManager.AddModifier(modifier);
            }
        }
    }
    public static void CheckConflicts(Modifier newModifier) {
        if (newModifier.name == "Major Housing Shortage") {
            foreach (Modifier modifier in ResourceManager.modifiers) {
                if (modifier.name == "Housing Shortage") {
                    ResourceManager.modifiers.Remove(modifier);
                }
            }
        }
    }
    
}
