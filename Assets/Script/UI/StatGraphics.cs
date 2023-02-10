using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatGraphics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statAmount;

    public void Setup(string name, int amount)
    {
        statName.text = name;
        statAmount.text = amount.ToString();

        if(amount >= 5)
        {
            statName.color = Color.yellow;
            statAmount.color = Color.yellow;
        }
    }
}
