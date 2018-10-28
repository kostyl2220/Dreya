using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dress : Item {
    [SerializeField] private DressType m_type;
    [SerializeField] private Protection m_protection;

    public enum DressType
    {
        DressType_Helmet,
        DressType_Skirt,
        DressType_Pants,
        DressType_Boots,
        DressType_Count
    }

    public DressType GetDressType()
    {
        return m_type;
    }

    public Protection GetProtection()
    {
        return m_protection;
    }

    public virtual void AddAbilities(Inventory inventory)
    {
        inventory.AddProtection(m_protection);
    }
}
