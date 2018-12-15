using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickable
{
    [SerializeField] private Collider m_pickingCollider;
    [SerializeField] private Collider m_bodyCollider;
    [SerializeField] private GameObject m_rootElement;
    [SerializeField] private Rigidbody m_rigidbody;

    private bool m_isPicked;
    protected Inventory m_inventory;

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
        if (!m_rigidbody)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }

    public Rigidbody GetRigidBody()
    {
        return m_rigidbody;
    }

    public void SetInventory(Inventory inv)
    {
        m_inventory = inv;
    }

    public virtual void Picked()
    {

    }

    public GameObject GetRoot()
    {
        return m_rootElement;
    }

    public void SetItemPicked(bool picked)
    {
        m_isPicked = picked;
        if (m_bodyCollider)
        {
            m_pickingCollider.enabled = !picked;
        }
        if (m_bodyCollider)
        {
            m_bodyCollider.isTrigger = picked;
        }
        if (m_rigidbody)
        {
            m_rigidbody.isKinematic = picked;
        }
    }

    public bool IsPicked()
    {
        return m_isPicked;
    }

    protected override bool PickedByPlayer(GameObject player)
    {
        if (m_isPicked)
        {
            return false;
        }

        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory)
        {
            inventory.PickItem(this);
        }

        return true;
    }

    protected override bool ShouldDestroyAfterInteraction()
    {
        return false;
    }
}
