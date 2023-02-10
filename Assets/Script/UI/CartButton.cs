using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class CartButton : MonoBehaviour
{
    [SerializeField] private Button cartButton;

    private void Awake()
    {
        cartButton.onClick.AddListener(OnCartButtonClicked);
    }

    private void OnCartButtonClicked()
    {
        var uiManager = ServiceLocator.GetService<UIManager>();
        uiManager.OpenCart();
    }
}
