using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickable {

    protected override void PickedByPlayer(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory)
        {
            inventory.PickItem(this);
        }
    }

    protected override bool ShouldDestroyAfterInteraction()
    {
        return false;
    }
}
