using NaughtyAttributes;
using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Monolog With Action", menuName = "Interactable Action/Player Monolog With Action")]
public class PlayerMonologActionAndCallOtherAction : InteractableActions
{
    [ResizableTextArea]
    [SerializeField] private string text;
    [SerializeField] private float modifier;
    [SerializeField] private InteractableActions action;

    public override void Execute(string text, float modifier)
    {
        ServiceLocator.GetService<PlayerController>().CreateMonolog(text, modifier);
        action.Execute(this.text, this.modifier);
    }
}