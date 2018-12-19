using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemsSwitcher : Switcher {

    [SerializeField] private List<Item> m_itemsToGive;
    [SerializeField] private Inventory m_playerInventory;

    protected override void Switched()
    {
        if (m_playerInventory)
        {
            m_itemsToGive.ForEach((Item i) => { m_playerInventory.PickItem(i); });
        }
    }

    // Use this for initialization
    new void Start () {
        if (!m_playerInventory)
        {
            m_playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        }
	}
}
