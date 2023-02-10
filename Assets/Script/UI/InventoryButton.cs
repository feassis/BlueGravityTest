using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;

    private void Awake()
    {
        inventoryButton.onClick.AddListener(OnInventoryButtonClicked);
    }

    private void OnInventoryButtonClicked()
    {
        var uiManager = ServiceLocator.GetService<UIManager>();
        uiManager.CloseAllUI();
        uiManager.OpenInventory(false);
    }
}
