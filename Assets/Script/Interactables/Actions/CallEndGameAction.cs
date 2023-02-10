using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Call Ending", menuName = "Interactable Action/Ending")]
public class CallEndGameAction : InteractableActions
{
    [ShowAssetPreview]
    [SerializeField] private Sprite bgImage;
    [ResizableTextArea]
    [SerializeField] private string endGameText;
     public override void Execute(string text, float modifier)
    {
        EndGameGraphics.Open(bgImage, endGameText);
    }
}