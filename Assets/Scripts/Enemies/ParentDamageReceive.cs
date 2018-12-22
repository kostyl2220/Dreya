using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDamageReceive : DamageReceiveComponent {

    [SerializeField] private DamageReceiveComponent m_parent;
    
    public void SetParent(DamageReceiveComponent parent)
    {
        m_parent = parent;
    }

    public override bool GetDamage(float damage, Vector3 pushForce, GameObject attacker)
    {
        bool alive = base.GetDamage(damage, pushForce, attacker);
        m_parent.GetDamage(damage, pushForce, attacker);
        return alive;
    }
}
