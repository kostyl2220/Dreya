using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Protection
{
    float fear;
    float noise;

    public static Protection operator +(Protection prot1, Protection prot2)
    {
        Protection newP;
        newP.fear = prot1.fear + prot2.fear;
        newP.noise = prot1.noise + prot2.noise;
        return newP;
    }

    public static Protection operator -(Protection prot1, Protection prot2)
    {
        Protection newP;
        newP.fear = Mathf.Max(prot1.fear - prot2.fear, 0);
        newP.noise = Mathf.Max(prot1.noise - prot2.noise, 0);
        return newP;
    }
}


public class Wardrobe : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_dressPoses = new Transform[(int)Dress.DressType.DressType_Count];

    private Dress[] m_dresses;
    private Protection m_generalProtection;

    public Dress DressItem(Dress item)
    {
        Dress oldDress = null;
        int itemId = (int)item.GetDressType();
        if (m_dresses[itemId])
        {
            oldDress = m_dresses[itemId];
            if (oldDress == item) // never should happen
            {
                return null;
            }
            RemoveProtection(oldDress.GetProtection());
        }
        item.GetRoot().transform.SetParent(m_dressPoses[itemId]);
        item.GetRoot().transform.localPosition = Vector3.zero;
        item.GetRoot().transform.localRotation = Quaternion.identity;
        item.GetRoot().SetActive(true);
        m_dresses[itemId] = item;
        AddProtection(item.GetProtection());

        return oldDress;
    }

    public Dress UndressItem(Dress.DressType type)
    {
        Dress currentDress = m_dresses[(int)type];
        if (currentDress)
        {
            RemoveProtection(currentDress.GetProtection());
            currentDress.gameObject.SetActive(false);
            m_dresses[(int)type] = null;
        }
        return currentDress;
    }

    public void AddProtection(Protection protection)
    {
        m_generalProtection += protection;
    }

    public void RemoveProtection(Protection protection)
    {
        m_generalProtection -= protection;
    }

    // Use this for initialization
    void Start () {
        m_dresses = new Dress[(int)Dress.DressType.DressType_Count];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
