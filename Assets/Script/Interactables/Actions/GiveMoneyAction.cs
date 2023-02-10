using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Give Money Action", menuName = "Interactable Action/Give Money")]
public class GiveMoneyAction : InteractableActions
{
    public override void Execute(string text, float modifier)
    {
        ServiceLocator.GetService<Wallet>().AddMoney(Mathf.RoundToInt(modifier));
    }
}
