using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

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
        m_items.Add(item);
        item.SetItemPicked(true);
        item.GetRoot().transform.position = gameObject.transform.position;
        item.GetRoot().transform.SetParent(gameObject.transform);       
        item.GetRoot().SetActive(false);
    }

    public void ThrowItem(Item item)
    {
        m_items.Remove(item);
        item.SetItemPicked(false);
        item.GetRoot().transform.SetParent(null);
        item.GetRoot().transform.position = gameObject.transform.position;
        item.GetRoot().SetActive(true);
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

    //Refactor this mehtod
    private void StartUsingItem(Item item)
    {
        if (item is Dress)
        {
            Dress dr = (Dress)item;
            m_wardrobe.DressItem(dr);
        }
        else if (item is Weapon)
        {
            Weapon wp = (Weapon)item;
            m_attackComp.SetRightHandWeapon(wp);
        }
    }

    //Refactor this mehtod
    private void StopUsingItem(Item item)
    {
        if (item is Dress)
        {
            Dress dr = (Dress)item;
            m_wardrobe.UndressItem(dr.GetDressType());
        }
        else if (item is Weapon)
        {
            Weapon wp = (Weapon)item;
            m_attackComp.SetRightHandWeapon(null);
        }
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_items.Count >= 1)
            {
                StartUsingItem(m_items[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_items.Count >= 1)
            {
                StopUsingItem(m_items[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_items.Count >= 1)
            {
                ThrowItem(m_items[0]);
            }
        }
    }
}
