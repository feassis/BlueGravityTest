using System;
using UnityEngine;

[Serializable]
public class CharacterInteractionTrigger
{
    public InputDirection TriggerDirection;
    public PlayerTrigger Trigger;

    public void SetActive(bool active)
    {
        Trigger.gameObject.SetActive(active);
    }
}