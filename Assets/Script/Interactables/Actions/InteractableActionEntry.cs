using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public struct InteractableActionEntry
{
    [ResizableTextArea]
    [SerializeField] private string ActionText;
    [SerializeField] private float Modifier;
    public InteractableActions Action;

    public void Execute()
    {
        Action.Execute(ActionText, Modifier);
    }
}