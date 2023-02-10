using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CartGraphics cartUI;
    [SerializeField] private ShopkeeperDialogboxGraphics dialogBox;
    [SerializeField] private InventoryGraphics inventory;
    [SerializeField] private StoreStandGraphics storeStand;
    [SerializeField] private YesOrNoDialogGraphics YesNoDialog;

    private void Awake()
    {
        ServiceLocator.RegisterService<UIManager>(this);
    }
    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<UIManager>();
    }

    public void OpenCart()
    {
        CloseAllUI();
        cartUI.Open();
    }

    public void CloseCart()
    {
        cartUI.Close();
    }

    public void OpenDialogBox()
    {
        CloseAllUI();
        dialogBox.Open();
    }

    public void CloseDialogBox()
    {
        dialogBox.Close();
    }

    public void OpenInventory(bool isSellingEnabled)
    {
        CloseAllUI();
        inventory.Open(isSellingEnabled);
    }

    public void CloseInventory()
    {
        inventory.Close();
    }

    public void OpenStoreStand(ProductType type)
    {
        CloseAllUI();
        storeStand.Open(type);
    }

    public void CloseStoreStand()
    {
        storeStand.Close();
    }

    public void OpenYesNoDialog(Action onYesAction, Action onNoAction, string dialogText)
    {
        CloseAllUI();
        YesNoDialog.Open(onYesAction, onNoAction, dialogText);
    }

    public void CloseYesNoDialog()
    {
        YesNoDialog.Close();
    }

    public void CloseAllUI()
    {
        CloseCart();
        CloseDialogBox();
        CloseInventory();
        CloseStoreStand();
        CloseYesNoDialog();
    }

    public void CloseUIClosableByMovement()
    {
        CloseDialogBox();
        CloseInventory();
        CloseStoreStand();
    }
}
