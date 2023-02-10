using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Monolog", menuName = "Interactable Action/Player Monolog")]
public class PlayerMonologAction : InteractableActions
{
    public override void Execute(string text, float modifier)
    {
        ServiceLocator.GetService<PlayerController>().CreateMonolog(text, modifier);
    }
}
