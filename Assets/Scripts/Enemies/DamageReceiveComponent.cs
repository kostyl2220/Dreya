using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveComponent : MonoBehaviour {
    private static string ANY_TAG_TYPE = "Any";
    private static float HP_EXP_MULTIPLIER = 5.0f;

    [SerializeField] private string m_tagGamageName = ANY_TAG_TYPE;
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

    public void SetHPProportion(float proportion)
    {
        m_currentHP = m_maxHP * proportion;
        GetDamage(0, Vector3.zero, null);
    }

    public bool CanBeDamaged(string tagName)
    {
        return m_tagGamageName == ANY_TAG_TYPE || tagName == m_tagGamageName;
    }

    public virtual bool GetDamage(float damage, Vector3 pushForce, GameObject attacker)
    {
        m_currentHP -= damage;
        Debug.Log(m_currentHP);

        bool isAlive = m_currentHP > 0.0f;

        if (isAlive)
        {
            if (OnHit != null)
            {
                OnHit.Invoke(damage, pushForce);
            }
        }
        else
        {
            if (OnDeath != null)
            {
                OnDeath.Invoke();
            }

            if (attacker)
            {
                PlayerSkills skills = attacker.GetComponent<PlayerSkills>();
                if (skills)
                {
                    skills.GetExp(m_maxHP * HP_EXP_MULTIPLIER);
                }
            }

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
