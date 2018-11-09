﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExistingSwitcher : Interactable {

    [SerializeField] private bool m_consumeItems = true;
    [SerializeField] private List<Item> m_shouldPlayerHave;
    [SerializeField] private List<Switcher> m_itemsToSwitch;

    protected override void InteractWithPlayer(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory && inventory.AllItemsExists(m_shouldPlayerHave))
        {
            m_itemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
            if (m_consumeItems)
            {
                m_shouldPlayerHave.ForEach((Item i) => inventory.ConsumeItem(i));
            }
        }
    }
}