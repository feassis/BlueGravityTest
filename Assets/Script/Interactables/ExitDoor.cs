using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableActionEntry hasItemsOnCartAction;
    [SerializeField] private InteractableActionEntry hasNoItemsOnCartAction;
    public void Interact()
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        bool hasItemsOnCart = inventory.GetItensWithStatus(ProductOnInventoryStatus.InCart).Count > 0;

        if (hasItemsOnCart)
        {
            hasItemsOnCartAction.Execute();
            return;
        }

        hasNoItemsOnCartAction.Execute();
    }
}
