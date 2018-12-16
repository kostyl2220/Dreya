using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Pickable
{
    [SerializeField] private Collider m_pickingCollider;
    [SerializeField] private Collider m_bodyCollider;
    [SerializeField] private GameObject m_rootElement;
    [SerializeField] private Rigidbody m_rigidbody;

    protected Inventory m_inventory;

    protected new void Start()
    {
        base.Start();
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

    protected new void OnTriggerEnter(Collider collision)
    {
        if (m_isPicked)
        {
            return;
        }

        base.OnTriggerEnter(collision);
    }

    protected new void OnTriggerExit(Collider collision)
    {
        if (m_isPicked)
        {
            return;
        }

        base.OnTriggerExit(collision);
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
