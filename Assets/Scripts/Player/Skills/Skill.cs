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

    [SerializeField] private bool m_active;
    [SerializeField] private Material m_icon;
    [SerializeField] private SkillType m_type;
    [SerializeField] protected float[] m_values = new float[MAX_LEVEL + 1];
    protected int m_level = 1;
    protected float m_nextUpdate;

    public int GetId()
    {
        return (int)m_type;
    }

    public virtual void InitSkill(GameObject player)
    {

    }

    public SkillType GetSkillType()
    {
        return m_type;
    }

    public virtual float GetCooldown()
    {
        return 1.0f;
    }

    public float GetNextUpdateTime()
    {
        return m_nextUpdate;
    }

    public bool IsActive()
    {
        return m_active;
    }

    public void LevelUp()
    {
        if (m_level == MAX_LEVEL)
        {
            return;
        }
        ++m_level;
    }

    public int GetLevel()
    {
        return m_level;
    }

    public Material GetIconMaterial()
    {
        return m_icon;
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

}
