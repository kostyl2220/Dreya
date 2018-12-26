using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExistingSwitcher : Interactable {

    [SerializeField] private bool m_consumeItems = true;
    [SerializeField] private List<Item> m_shouldPlayerHave;
    [SerializeField] private List<Switcher> m_successItemsToSwitch;
    [SerializeField] private List<Switcher> m_failItemsToSwitch;
    [SerializeField] private bool m_atleastOne = false;

    protected override void InteractWithPlayer(GameObject player)
    {
        if (m_shouldPlayerHave.Count == 0)
        {
            m_successItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
            return;
        }

        //TODO REFACTOR
        Inventory inventory = player.GetComponent<Inventory>();
        if (!m_atleastOne)
        {
            if (inventory && inventory.AllItemsExists(m_shouldPlayerHave))
            {
                m_successItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
                if (m_consumeItems)
                {
                    m_shouldPlayerHave.ForEach((Item i) => inventory.ConsumeItem(i));
                }
            }
            else
            {
                m_failItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
            }
        }
        else
        {
            Item item = inventory.AtLeastOneExists(m_shouldPlayerHave);
            if (item)
            {
                m_successItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
                if (m_consumeItems)
                {
                    inventory.ConsumeItem(item);
                }
            }
            else
            {
                m_failItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
            }
        }
    }
}
