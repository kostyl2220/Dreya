using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Protection
{
    float fear;
    float noise;

    public static Protection operator+(Protection prot1, Protection prot2)
    {
        Protection newP;
        newP.fear = prot1.fear + prot2.fear;
        newP.noise = prot1.noise + prot2.noise;
        return newP;
    }

    public static Protection operator-(Protection prot1, Protection prot2)
    {
        Protection newP;
        newP.fear = Mathf.Max(prot1.fear - prot2.fear, 0) ;
        newP.noise = Mathf.Max(prot1.noise - prot2.noise, 0);
        return newP;
    }
}

public class Inventory : MonoBehaviour {

    [SerializeField]
    private Vector3 [] m_dressPoses = new Vector3[(int)Dress.DressType.DressType_Count];

    private Protection m_generalProtection;
    private bool m_reevaluateProtection;

    private List<Item> m_items;
    private Dress [] m_dresses;
	// Use this for initialization
	void Start ()
    {
        m_items = new List<Item>();
        m_dresses = new Dress[(int)Dress.DressType.DressType_Count];
        m_reevaluateProtection = true;
    }

    public void DressItem(Dress item)
    {
        int itemId = (int)item.GetDressType();
        if (m_dresses[itemId])
        {
            if (m_dresses[itemId] == item)
            {
                return;
            }
            RemoveProtection(m_dresses[itemId].GetProtection());
            m_dresses[itemId].gameObject.SetActive(false);
        }
        item.gameObject.transform.rotation = transform.rotation;
        item.gameObject.transform.position = transform.position + m_dressPoses[itemId];      
        item.gameObject.SetActive(true);
        m_dresses[itemId] = item;
        AddProtection(item.GetProtection());
    }

    public void UndressItem(Dress.DressType type)
    {
        Dress currentDress = m_dresses[(int)type];
        if (currentDress)
        {
            RemoveProtection(currentDress.GetProtection());
            currentDress.gameObject.SetActive(false);
            m_dresses[(int)type] = null;
        }
    }

    public void PickItem(Item item)
    {
        m_items.Add(item);
        item.gameObject.transform.position = gameObject.transform.position;
        item.gameObject.transform.SetParent(gameObject.transform);       
        item.gameObject.SetActive(false);
    }

    public void ThrowItem(Item item)
    {
        m_items.Remove(item);
        item.gameObject.transform.SetParent(null);
        item.gameObject.transform.position = gameObject.transform.position;
        item.gameObject.SetActive(true);
    }

    public void AddProtection(Protection protection)
    {
        m_generalProtection += protection;
    }

    public void RemoveProtection(Protection protection)
    {
        m_generalProtection -= protection;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_items.Count >= 1)
            {
                DressItem((Dress)m_items[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_items.Count >= 1)
            {
                UndressItem(Dress.DressType.DressType_Helmet);
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
