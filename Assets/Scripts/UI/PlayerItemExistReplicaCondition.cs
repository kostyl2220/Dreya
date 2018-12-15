using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemExistReplicaCondition : DialogManager.ReplicaCondition
{
    [SerializeField] private List<Item> m_needingItems;
    [SerializeField] private int m_enoughCount;

    public override bool ProcessPlayer(GameObject player)
    {
        Inventory inv = player.GetComponent<Inventory>();
        if (!inv)
        {
            return false;
        }

        int count = 0;
        inv.GetAllItems().ForEach(
            (Item i) =>
            {
                if (m_needingItems.Contains(i))
                {
                    ++count;
                }
            });

        return count >= m_enoughCount;
    }
}
