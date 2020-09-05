using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Options
{
    public string text = "";
    public string effects = "";
    public List<Modifier> modifiers = new List<Modifier>();
    public string results = "";
    public string requirements = "";
    public string costs = "";
    public string next = "";

    //public Options() {}

    public void ParseEffects() {
        string[] list = effects.Split(';');
        for(int i = 0; i < list.Length; i++) {
            string[] effect = list[i].Trim().Split('=');
            switch(effect[0]) {
                case "unity":
                    ResourceManager.AddUnity(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "trust":
                    ResourceManager.AddTrust(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "unrest":
                    ResourceManager.AddUnrest(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "population":
                    ResourceManager.AddPopulation(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "food":
                    ResourceManager.AddFood(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "materials":
                    ResourceManager.AddMaterials(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "machinery":
                    ResourceManager.AddMachinery(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
            }

        }
    }
}
