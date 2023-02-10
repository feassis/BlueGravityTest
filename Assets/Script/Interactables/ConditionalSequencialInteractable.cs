using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalSequencialInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<ConditionInteractableActionEntry> actions = new List<ConditionInteractableActionEntry>();

    private int index = 0;

    [Serializable]
    private struct ConditionInteractableActionEntry
    {
        public InteractableCondition condition;

        public InteractableActionEntry action;
    }

    public void Interact()
    {
        bool condition = actions[index].condition != null && actions[index].condition.HasMetCondition();
        actions[index].action.Execute();


        if (condition || actions[index].condition == null)
        {
            index = Mathf.Clamp(index + 1, 0, actions.Count - 1);
        }  
    }
}
