using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickable
{
    [SerializeField] private Collider m_pickingCollider;
    [SerializeField] private GameObject m_rootElement;

    private bool m_isPicked;

    protected void Start()
    {
        if (!m_pickingCollider)
        {
            m_pickingCollider = GetComponent<Collider>();
        }
        if (!m_rootElement)
        {
            m_rootElement = gameObject;
        }
    }

    public virtual void Use()
    {

    }

    public GameObject GetRoot()
    {
        return m_rootElement;
    }

    public void SetItemPicked(bool picked)
    {
        m_isPicked = picked;
        m_pickingCollider.enabled = !picked;
    }

    public bool IsPicked()
    {
        return m_isPicked;
    }

    protected override void PickedByPlayer(GameObject player)
    {
        if (m_isPicked)
        {
            return;
        }

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
