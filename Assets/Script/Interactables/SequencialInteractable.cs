using System.Collections.Generic;
using UnityEngine;

public class SequencialInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<InteractableActionEntry> actions = new List<InteractableActionEntry>();

    private int index = 0;
    public void Interact()
    {
        actions[index].Execute();
        index = Mathf.Clamp(index + 1, 0, actions.Count - 1);
    }
}
