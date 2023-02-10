using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        ServiceLocator.GetService<UIManager>().OpenDialogBox();
    }
}
