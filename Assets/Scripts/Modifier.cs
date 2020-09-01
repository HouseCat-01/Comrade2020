using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier
{
    public string name = "";

    public abstract void OnAdd();
    public abstract void Reoccuring();
    public abstract void Constant();
}
