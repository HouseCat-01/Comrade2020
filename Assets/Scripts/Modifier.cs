using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Modifier
{
    //public ResourceManager.Tag tag;
    public void OnTurn() { } //effects that occur when turn is advanced
    public void Constant() { } //constant modifiers
    public void OnAdd() { } //effects when modifier is first added
    public void OnRemove() { } //effects when modifier is removed
}
