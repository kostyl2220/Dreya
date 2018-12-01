using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dress : Exchangable
{
    [SerializeField] private DressType m_type;
    [SerializeField] private Protection m_protection;

    private Wardrobe m_wardrobe;
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

    public override void Picked()
    {
        base.Picked();
        m_inventory.PickDress(this);
    }
}
