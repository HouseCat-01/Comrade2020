using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Modifier
{
    public string name = "";
    public string onAdd = "";
    public string effects = "";
    public string continuous = "";
    public string requirements = "";
    public string onRemove = "";

    public Modifier() { }
    public Modifier(string name) {
        this.name = name;
    }
    public Modifier(string name, string effects) {
        this.name = name;
        this.effects = effects;
    }
    public Modifier(string name, string effects, string requirements) {
        this.name = name;
        this.effects = effects;
        this.requirements = requirements;
    }
    public Modifier(string name, string effects, string continuous, string requirements) {
        this.name = name;
        this.effects = effects;
        this.continuous = continuous;
        this.requirements = requirements;
    }

    public void OnAdd() {
        ProcessText(onAdd);
        ProcessText(effects);
    }
    public void OnRemove() {
        ProcessText(effects);
        ProcessText(onRemove);
    }
    public void ContinuousProccess() {
        ProcessText(continuous);
    }
    //returns true if all requirements for keeping modifier are met
    public bool CheckRequirements() {
        string[] list = requirements.Split(';');
        for (int i = 0; i < list.Length; i++) {
            string[] effect = list[i].Trim().Split('=');
            switch (effect[0]) {
                case "unity":
                    switch(effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetUnity() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetUnity() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetUnity() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "trust":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetTrust() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetTrust() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetTrust() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "unrest":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetUnrest() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetUnrest() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetUnrest() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "population":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetPopulation() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetPopulation() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetPopulation() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "food":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetFood() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetFood() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetFood() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "materials":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetMaterials() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetMaterials() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetMaterials() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;
                case "machinery":
                    switch (effect[1][0]) {
                        case '<':
                            if (ResourceManager.GetMachinery() > int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '>':
                            if (ResourceManager.GetMachinery() < int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                        case '=':
                            if (ResourceManager.GetMachinery() != int.Parse(effect[1].Substring(1), NumberStyles.AllowLeadingSign)) return false;
                            break;
                    }
                    break;

            }
        }
        return true;
    }

    public void ProcessText(string effects) {
        string[] list = continuous.Split(';');
        for (int i = 0; i < list.Length; i++) {
            string[] effect = list[i].Trim().Split('=');
            switch (effect[0]) {
                case "unity":
                    ResourceManager.AddUnity(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "trust":
                    ResourceManager.AddTrust(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "unrest":
                    ResourceManager.AddUnrest(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "unrestChange":
                    ResourceManager.AddUnrestChange(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
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
                case "foodIncome":
                    ResourceManager.AddFoodIncome(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "materialsIncome":
                    ResourceManager.AddMaterialsIncome(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                case "machineryIncome":
                    ResourceManager.AddMachineryIncome(int.Parse(effect[1], NumberStyles.AllowLeadingSign));
                    break;
                    
            }
        }
    }
}
