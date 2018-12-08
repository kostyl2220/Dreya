using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Exchangable
{
    private static float INFINITE_DAMAGE = 100000.0f;

    [SerializeField] public float m_cooldown = 1.0f;
    [SerializeField] public float m_damage = 20.0f;
    [SerializeField] public float m_pushForce = 4.0f;
    [SerializeField] private Animation m_animation;
    [SerializeField] private string m_simpleAttack = "SimpleAttack";

    private bool m_isAttacking;
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

    public void AttackFinished()
    {
        m_isAttacking = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!m_isAttacking)
        {
            return;
        }

        DamageReceiveComponent drc = collider.gameObject.GetComponent<DamageReceiveComponent>();
        if (drc)
        {
            float actualDamage = m_damage;
            Vector3 attackDirection = (collider.gameObject.transform.position - m_attackComp.GetAttacker().transform.position).normalized;
            drc.GetDamage(m_attackComp.PerformCriticalHit() ? INFINITE_DAMAGE : actualDamage, attackDirection * m_pushForce);
            m_attackComp.HitPerformed(actualDamage);
        }
    }

    public override void Picked()
    {
        base.Picked();
        m_inventory.PickWeapon(this);
    }

    public void Hit()
    {
        m_animation.Stop();
        m_animation.Play(m_simpleAttack);
        m_isAttacking = true;
    }
}
