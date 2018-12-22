using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeaponsSwitcher : Switcher
{
    [SerializeField] private List<GameObject> m_itemsToGive;
    [SerializeField] private Inventory m_playerInventory;

    protected override void Switched()
    {
        if (m_playerInventory)
        {
            m_itemsToGive.ForEach((GameObject hw) => 
            {
                GameObject hackWeapon = Instantiate(hw);
                HackWeaponHolder hwh = hackWeapon.GetComponent<HackWeaponHolder>();
                if (hwh)
                {
                    m_playerInventory.PickItem(hwh.GetWeapon());
                }
            });
        }
    }

    // Use this for initialization
    new void Start()
    {
        if (!m_playerInventory)
        {
            m_playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        }
    }
}
