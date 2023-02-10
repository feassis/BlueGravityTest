using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Consume Item And Do Something", menuName = "Interactable Action/Consume Item Do Something")]
public class ConsumeNamedItemAndDoSomethingAction : InteractableActions
{
    [SerializeField] private string itemName;
    [SerializeField] private InteractableActions IfHasItemAction;

    [SerializeField] private string caseNegativeText;
    [SerializeField] private float caseNegativeModifier;
    [SerializeField] private InteractableActions IfHasNotItemAction;

    public override void Execute(string text, float modifier)
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        bool hasItem = inventory.HasNamedItem(itemName);

        if (hasItem)
        {
            inventory.RemoveNamedItem(itemName);

            IfHasItemAction.Execute(text, modifier);
        }
        else
        {
            IfHasNotItemAction.Execute(caseNegativeText, caseNegativeModifier);
        }
    }
}

