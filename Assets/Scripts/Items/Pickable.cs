using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : Interactable
{
    protected abstract bool PickedByPlayer(GameObject player);

    protected override void InteractWithPlayer(GameObject player)
    {
        if (PickedByPlayer(player))
        {
            SimpleCharacterControl scc = player.GetComponent<SimpleCharacterControl>();
            if (scc)
            {
                scc.PickUp();
            }
        }
    }

    protected override bool ShouldDestroyAfterInteraction()
    {
        return true;
    }
}
