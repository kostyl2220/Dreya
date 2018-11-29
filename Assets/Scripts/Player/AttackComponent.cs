using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour {

    [SerializeField] private GameObject m_staticAttackPoint;
    [SerializeField] private GameObject m_rightHandSlot;
    private Weapon m_rightHandWeapon;
    private float m_lastHitCooldown;

    // Use this for initialization
    void Start ()
    {
        m_lastHitCooldown = 0;
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
        m_rightHandWeapon.Hit();
    }

    // Update is called once per frame
    void Update () {
        UpdateRightHandWeapon();
    }
}
