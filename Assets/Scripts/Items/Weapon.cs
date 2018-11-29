using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] public float m_cooldown = 1.0f;
    [SerializeField] public float m_damage = 20.0f;
    [SerializeField] private Animation m_animation;
    [SerializeField] private string m_simpleAttack = "SimpleAttack";

    private AttackComponent m_attackComp;
    // Use this for initialization
    new void Start ()
    {
        base.Start();
        if (!m_animation)
        {
            m_animation = GetComponent<Animation>();
        }
	}

    public void SetAttackComponent(AttackComponent ac)
    {
        m_attackComp = ac;
    }

    public void Return()
    {
        m_attackComp.ResetParent();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!m_animation.isPlaying)
        {
            return;
        }

        DamageReceiveComponent drc = collider.gameObject.GetComponent<DamageReceiveComponent>();
        if (drc)
        {
            drc.GetDamage(m_damage);
        }
    }

    public override void Picked()
    {
        m_inventory.PickWeapon(this);
    }

    public void Hit()
    {
        m_animation.Stop();
        m_animation.Play(m_simpleAttack);
    }
}
