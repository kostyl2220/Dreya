using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {
    public static int MAX_LEVEL = 2;

    public enum SkillType
    {
        AdditionalHP,
        Inspiration,
        CriticalHit,
        HolyLight,
        LightHider,
        Blanket,
        SplashLight,
        IamMonster,
        Count
    };

    [SerializeField] private SkillType m_type;
    [SerializeField] protected float[] m_values = new float[MAX_LEVEL + 1];
    protected int m_level;

    public int GetId()
    {
        return (int)m_type;
    }

    public void LevelUp()
    {
        if (m_level == MAX_LEVEL)
        {
            return;
        }
        ++m_level;
    }

    public bool Downgrade()
    {
        --m_level;
        return m_level != -1;
    }

    public virtual void Use()
    {

    }

    public virtual float GetEffect()
    {
        return m_values[m_level];
    }

	// Use this for initialization
	protected void Start () {
        m_level = 0;
	}

}
