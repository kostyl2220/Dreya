using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : Interactable
{
    protected abstract void PickedByPlayer(GameObject player);

    protected override void InteractWithPlayer(GameObject player)
    {
        SimpleCharacterControl scc = player.GetComponent<SimpleCharacterControl>();
        if (scc)
        {
            scc.PickUp();
            PickedByPlayer(player);
        }
    }

    protected override bool ShouldDestroyAfterInteraction()
    {
        return true;
    }
}
