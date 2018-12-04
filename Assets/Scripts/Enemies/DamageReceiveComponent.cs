using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveComponent : MonoBehaviour {

    public delegate void HitEvent(float damage, Vector3 pushForce);
    public event HitEvent OnDeath;
    public event HitEvent OnHit;

    [SerializeField] private float m_maxHP;
    private float m_currentHP;

    public bool GetDamage(float damage, Vector3 pushForce)
    {
        m_currentHP -= damage;
        bool isAlive = m_currentHP > 0.0f;

        HitEvent evnt = isAlive ? OnHit : OnDeath;
        if (evnt != null)
        {
            evnt.Invoke(damage, pushForce);
        }

        return isAlive;
    }

	// Use this for initialization
	void Start ()
    {
        m_currentHP = m_maxHP;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
