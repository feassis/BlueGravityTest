using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class StoreStand : MonoBehaviour, IInteractable
{
    [SerializeField] private ProductType productType;
    public void Interact()
    {
        var uiManager = ServiceLocator.GetService<UIManager>();

        uiManager.OpenStoreStand(productType);
    }
}
