using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCorrectionSwither : Switcher {

    [SerializeField] private GameObject m_boss;
    [SerializeField] private List<Item> m_needingItems;

    private GameObject m_player;

    protected override void Switched()
    {
        DamageReceiveComponent drc = m_boss.GetComponent<DamageReceiveComponent>();
        float percentage = 1.0f - GetItemCount(m_player) / (float)m_needingItems.Count;
        if (drc)
        {
            drc.SetHPProportion(percentage);
        }
    }

    private int GetItemCount(GameObject player)
    {
        Inventory inv = player.GetComponent<Inventory>();
        if (!inv)
        {
            return 0;
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

        return count;
    }

    new void Start()
    {
        m_player = GameObject.Find("Player");
    }
}
