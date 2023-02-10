using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonetaryChangeGraphics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    public void Setup(int amount)
    {
        amountText.text = amount.ToString();

        if(amount >= 0)
        {
            amountText.color = Color.green;
            return;
        }

        amountText.color = Color.red;
    }
}
