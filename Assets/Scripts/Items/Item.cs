using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    protected override void InteractWithPlayer(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory)
        {
            inventory.PickItem(this);
        }
    }
}
