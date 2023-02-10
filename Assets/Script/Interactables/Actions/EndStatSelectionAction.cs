using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "End Stat Selection Action", menuName = "Interactable Action/End Stat Selection Action")]
public class EndStatSelectionAction : InteractableActions
{
    [SerializeField] private InteractableActions maxCool;
    [SerializeField] private InteractableActions maxBadass;
    [SerializeField] private InteractableActions maxCute;
    [SerializeField] private InteractableActions maxCuteAndBadass;
    [SerializeField] private InteractableActions maxCuteAndCool;
    [SerializeField] private InteractableActions maxBadassCool;
    [SerializeField] private InteractableActions noItemsBought;
    [SerializeField] private InteractableActions nothingSpecial;

    public override void Execute(string text, float modifier)
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        if(inventory.GetAllItensOnInventory().Count == 0)
        {
            noItemsBought.Execute(text, modifier);
            return;
        }

        var player = ServiceLocator.GetService<PlayerController>();

        var stats = player.GetPlayerStats();

        if (stats.badassery >=5 && stats.coolness >=5)
        {
            maxBadassCool.Execute(text, modifier);
            return;
        }

        if(stats.badassery >= 5 && stats.cuteness >= 5)
        {
            maxCuteAndBadass.Execute(text, modifier);
            return;
        }

        if(stats.coolness >= 5 && stats.cuteness >= 5)
        {
            maxCuteAndCool.Execute(text, modifier);
            return;
        }

        if (stats.badassery >= 5)
        {
            maxBadass.Execute(text, modifier);
            return;
        }

        if (stats.cuteness >= 5)
        {
            maxCute.Execute(text, modifier);
            return;
        }

        if (stats.coolness >= 5 )
        {
            maxCool.Execute(text, modifier);
            return;
        }

        nothingSpecial.Execute(text, modifier);
    }
}
