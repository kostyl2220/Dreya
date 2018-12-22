using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour {

    [SerializeField] private GameObject m_staticAttackPoint;
    [SerializeField] private GameObject m_rightHandSlot;
    private Weapon m_rightHandWeapon;
    private float m_lastHitCooldown;
    private PlayerSkills m_playerSkills;
    private Fearable m_fearable;

    // Use this for initialization
    void Start ()
    {
        m_lastHitCooldown = 0;
        if (!m_playerSkills)
        {
            m_playerSkills = GetComponent<PlayerSkills>();
        }
        if (!m_fearable)
        {
            m_fearable = GetComponent<Fearable>();
        }
    }

    public bool PerformCriticalHit()
    {
        if (!m_playerSkills)
        {
            return false;
        }

        Skill criticalHit = m_playerSkills.GetSkill(Skill.SkillType.CriticalHit);

        if (!criticalHit)
        {
            return false;
        }

        return Random.Range(0, 1) < criticalHit.GetEffect();
    }

    public void HitPerformed(float damage)
    {
        if (!(m_playerSkills && m_fearable))
        {
            return;
        }

        Skill inspiration = m_playerSkills.GetSkill(Skill.SkillType.Inspiration);
        if (!inspiration)
        {
            return;
        }

        float fearDecreased = inspiration.GetEffect() * damage;
        m_fearable.ChangeFear(-fearDecreased);
    }

    public Weapon SetRightHandWeapon(Weapon weapon)
    {
        Weapon oldWeapon = null;
        if (m_rightHandWeapon)
        {
            oldWeapon = m_rightHandWeapon;
        }

        m_rightHandWeapon = weapon;
        if (m_rightHandWeapon)
        {
            m_rightHandWeapon.SetAttackComponent(this);
            m_rightHandWeapon.GetRoot().SetActive(true);
            ResetParent();
        }

        return oldWeapon;
    }

    public GameObject GetAttacker()
    {
        return gameObject;
    }

    public void ResetParent()
    {
        m_rightHandWeapon.GetRoot().transform.SetParent(m_rightHandSlot.transform);
        m_rightHandWeapon.GetRoot().transform.localPosition = Vector3.zero;
        m_rightHandWeapon.GetRoot().transform.localRotation = Quaternion.identity;
    }

    void UpdateRightHandWeapon()
    {
        if (!m_rightHandWeapon)
        {
            return;
        }

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }

        if (Time.time < m_lastHitCooldown)
        {
            return;
        }

        m_lastHitCooldown = Time.time + m_rightHandWeapon.m_cooldown;
        //m_staticAttackPoint.transform.position = m_rightHandSlot.transform.position;
        m_rightHandWeapon.GetRoot().transform.SetParent(m_staticAttackPoint.transform);
        m_rightHandWeapon.GetRoot().transform.localPosition = Vector3.zero;
        m_rightHandWeapon.GetRoot().transform.localRotation = Quaternion.identity;
        m_rightHandWeapon.Hit();
    }

    // Update is called once per frame
    void Update () {
        UpdateRightHandWeapon();
    }
}
