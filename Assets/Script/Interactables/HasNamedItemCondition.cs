using Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "has Item Contition", menuName = "InteractableCondition/Has Item")]
public class HasNamedItemCondition : InteractableCondition
{
    [SerializeField] private string itemName;
    public override bool HasMetCondition()
    {
        return ServiceLocator.GetService<Inventory>().HasNamedItem(itemName);
    }
}