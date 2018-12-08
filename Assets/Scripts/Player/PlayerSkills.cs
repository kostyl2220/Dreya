using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

    [SerializeField] private int m_startAvaliableSkills;
    [SerializeField] private SkillBar m_skillBar;

    public delegate void OnAddSkill();
    public event OnAddSkill m_onAddSkill;
    private Skill[] m_skills;
    public int m_avaliableSkills { get; set; }

    public void AddSkill(Skill newSkill)
    {
        Skill oldSkill = m_skills[newSkill.GetId()];
        if (oldSkill == null)
        {
            m_skills[newSkill.GetId()] = newSkill;
        }
        else
        {
            oldSkill.LevelUp();
        }

        if (m_onAddSkill != null)
        {
            m_onAddSkill.Invoke();
        }
    }

    public Skill GetSkill(Skill.SkillType type)
    {
        return m_skills[(int)type];
    }

    public void RemoveSkill(Skill.SkillType type)
    {
        Skill skill = m_skills[(int)type];
        if (skill == null)
        {
            return;
        }

        if (!skill.Downgrade())
        {
            Destroy(m_skills[(int)type]);
            m_skills[(int)type] = null;
        }
    }

    // Use this for initialization
    void Awake() {
        m_skills = new Skill[(int)Skill.SkillType.Count];
        m_avaliableSkills = m_startAvaliableSkills;
	}

    private void Start()
    {
        m_skillBar.SetAvaliableSkills(m_avaliableSkills);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
