using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private IInteractable interactableObject = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstants.InteractableTag))
        {
            collision.TryGetComponent<IInteractable>(out interactableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactionObject;

        if (collision.CompareTag(GameConstants.InteractableTag))
        {
            if(collision.TryGetComponent<IInteractable>(out interactionObject))
            {
                if(interactionObject == interactableObject)
                {
                    interactableObject = null;
                }
            }
        }
    }

    public void TryToInteract()
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }
}
