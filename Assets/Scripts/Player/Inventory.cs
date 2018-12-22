using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

    [SerializeField] private GameObject m_dropPoint;
    [SerializeField] private float m_dropForce = 1.0f;

    private List<Item> m_items;
    private AttackComponent m_attackComp;
    private Wardrobe m_wardrobe;
	// Use this for initialization
	void Start ()
    {
        m_items = new List<Item>();
        m_attackComp = GetComponent<AttackComponent>();
        m_wardrobe = GetComponent<Wardrobe>();
    }

    public void PickItem(Item item)
    {
        Pick(item);
        item.Picked();
    }

    public void PickWeapon(Weapon weapon)
    {
        Weapon oldWeapon = m_attackComp.SetRightHandWeapon(weapon);
        if (oldWeapon)
        {
            Throw(oldWeapon);
        }
    }

    public void PickDress(Dress dress)
    {
        Dress oldDress = m_wardrobe.DressItem(dress);
        if (oldDress)
        {
            Throw(oldDress);
        }
    }

    private void Pick(Item item)
    {
        m_items.Add(item);
        item.SetItemPicked(true);
        item.GetRoot().transform.position = gameObject.transform.position;
        item.GetRoot().transform.SetParent(gameObject.transform);       
        item.GetRoot().SetActive(false);
        item.SetInventory(this);
    }

    private void Throw(Exchangable exch)
    {
        exch.StartDestroying();
        Throw((Item)exch);
    }

    private void Throw(Item item)
    {
        m_items.Remove(item);
        item.SetItemPicked(false);
        item.GetRoot().transform.SetParent(null);
        item.GetRoot().transform.position = m_dropPoint.transform.position;
        item.GetRoot().SetActive(true);

        Rigidbody rb = item.GetRigidBody();
        if (rb)
        {
            rb.AddForce(m_dropPoint.transform.forward * m_dropForce);
        }

        item.SetInventory(null);
    }

    public List<Item> GetAllItems()
    {
        return m_items;
    }

    public void ConsumeItem(Item item)
    {
        m_items.Remove(item);
    }

    public bool AllItemsExists(List<Item> items)
    {
        return items.TrueForAll((Item i) => { return m_items.Contains(i); });
    }

    public Item AtLeastOneExists(List<Item> items)
    {         
        return items.Find((Item i) => { return m_items.Contains(i); });
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_items.Count >= 1)
            {
                Throw(m_items[0]);
            }
        }
    }
}
