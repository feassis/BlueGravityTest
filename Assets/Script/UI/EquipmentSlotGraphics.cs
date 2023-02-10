using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotGraphics : MonoBehaviour
{
    [SerializeField] private Image equipeditemImage;

    public void Setup(Product productEquipedOnSlot)
    {
        if(productEquipedOnSlot == null)
        {
            equipeditemImage.sprite = null;
            equipeditemImage.gameObject.SetActive(false);
            return;
        }

        equipeditemImage.sprite = productEquipedOnSlot.ProductImage;
        equipeditemImage.gameObject.SetActive(true);
    }
}
