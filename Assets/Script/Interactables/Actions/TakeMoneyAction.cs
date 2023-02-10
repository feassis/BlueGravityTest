using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Take Money Action", menuName = "Interactable Action/Take Money")]
public class TakeMoneyAction : InteractableActions
{
    public override void Execute(string text, float modifier)
    {
        ServiceLocator.GetService<Wallet>().SubtractMoney(Mathf.RoundToInt(modifier));
    }
}