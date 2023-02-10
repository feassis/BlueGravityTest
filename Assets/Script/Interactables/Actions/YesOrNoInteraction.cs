using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Yes Or No interaction", menuName = "Interactable Action/Yes Or No")]
public class YesOrNoInteraction : InteractableActions
{
    [SerializeField] private InteractableActionEntry yesInteraction;
    [SerializeField] private InteractableActionEntry noInteraction;

    public override void Execute(string text, float modifier)
    {
        var uiManager = ServiceLocator.GetService<UIManager>();

        Action yesAction = yesInteraction.Action == null ? null : yesInteraction.Execute;
        Action noAction = noInteraction.Action == null ? null : noInteraction.Execute;

        uiManager.OpenYesNoDialog(yesAction, noAction, text);
    }
}
