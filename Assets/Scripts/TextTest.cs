using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextTest : MonoBehaviour
{

    public void UnityTest() {
        ResourceManager.AddUnity(-10);
    }
    public void TrustTest() {
        ResourceManager.AddTrust(10);
    }
    public void UnrestTest() {
        ResourceManager.AddUnrest(10);
    }
    public void PopulationTest() {
        ResourceManager.AddPopulation(1000);
    }
    public void FoodTest() {
        ResourceManager.AddFood(10);
    }
    public void MaterialsTest() {
        ResourceManager.AddMaterials(10);
    }
    public void MachineryTest() {
        ResourceManager.AddMachinery(10);
    }
    public void DialogueTest() {
        SceneManager.LoadSceneAsync(2);
    }
}
