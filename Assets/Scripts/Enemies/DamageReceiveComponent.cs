using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveComponent : MonoBehaviour {

    [SerializeField] private bool m_killOnDeath = false;
    public delegate void HitEvent(float damage, Vector3 pushForce);
    public delegate void DeathEvent();
    public event DeathEvent OnDeath;
    public event HitEvent OnHit;

    [SerializeField] private float m_maxHP;
    private float m_currentHP;

    public float GetHP()
    {
        return m_currentHP;
    }

    public float GetMaxHP()
    {
        return m_maxHP;
    }

    public virtual bool GetDamage(float damage, Vector3 pushForce)
    {
        m_currentHP -= damage;
        bool isAlive = m_currentHP > 0.0f;

        if (isAlive)
        {
            if (OnHit != null)
            {
                OnHit.Invoke(damage, pushForce);
            }
        }
        else if (OnDeath != null)
        {
            OnDeath.Invoke();
            if (m_killOnDeath)
            {
                Destroy(gameObject);
            }
        }

        return isAlive;
    }

	// Use this for initialization
	protected void Start ()
    {
        m_currentHP = m_maxHP;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
