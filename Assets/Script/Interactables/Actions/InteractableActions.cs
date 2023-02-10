using System.Collections;
using UnityEngine;

public abstract class InteractableActions : ScriptableObject
{
    public abstract void Execute(string text, float modifier);
}
