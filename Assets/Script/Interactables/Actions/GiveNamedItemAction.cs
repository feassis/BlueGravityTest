using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Give Named Item", menuName = "Interactable Action/Give Named Action")]
public class GiveNamedItemAction : InteractableActions
{
    [SerializeField] private string itemName;

    public override void Execute(string text, float modifier)
    {
        ServiceLocator.GetService<Inventory>().AddNamedItem(itemName);
    }
}

