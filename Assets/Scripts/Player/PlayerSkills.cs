using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

    [SerializeField] private int m_startAvaliableSkills;
    [SerializeField] private SkillBar m_skillBar;
    [SerializeField] private SkillPanel m_skillPanel;

    public delegate void OnAddSkill();
    public event OnAddSkill m_onAddSkill;
    private Skill[] m_skills;
    private List<Skill> m_activeSkills;
    public int m_avaliableSkills { get; set; }

    public int AddSkill(Skill newSkill)
    {
        Skill currentSkill = m_skills[newSkill.GetId()];
        if (currentSkill == null)
        {
            m_skills[newSkill.GetId()] = newSkill;
            currentSkill = newSkill;
            if (currentSkill.IsActive())
            {
                currentSkill.InitSkill(gameObject);
                m_activeSkills.Add(currentSkill);
            }
        }
        else
        {
            currentSkill.LevelUp();
        }

        m_skillPanel.AddSkill(currentSkill);
        if (m_onAddSkill != null)
        {
            m_onAddSkill.Invoke();
        }

        return currentSkill.GetLevel();
    }

    public Skill GetSkill(Skill.SkillType type)
    {
        return m_skills[(int)type];
    }

    public bool UseSkill(Skill.SkillType type)
    {
        Skill skill = GetSkill(type);
        if (!skill)
        {
            return false;
        }

        if (!skill.IsActive())
        {
            return true;
        }

        if (Time.time < skill.GetNextUpdateTime())
        {
            return false;
        }

        //Use skill
        skill.Use();
        m_skillPanel.UseSkill(skill);

        return true;
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

    public Skill[] GetAllSkills()
    {
        return m_skills;
    }

    // Use this for initialization
    void Awake() {
        m_skills = new Skill[(int)Skill.SkillType.Count];
        m_avaliableSkills = m_startAvaliableSkills;
        m_activeSkills = new List<Skill>();
    }

    private void Start()
    {
        m_skillBar.SetAvaliableSkills(m_avaliableSkills);
    }

    // Update is called once per frame
    void Update () {
		for (int i = 0; i < m_activeSkills.Count; ++i)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i))) // get number on upper keyboard
            {
                UseSkill(m_activeSkills[i].GetSkillType());
            }
        }
	}
}
