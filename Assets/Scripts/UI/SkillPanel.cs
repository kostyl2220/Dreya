using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour {

    [SerializeField] private PanelUISkill m_panelSkillUnit;

    private PanelUISkill[] m_panelSkills;
	
    public void UseSkill(Skill skill)
    {
        PanelUISkill panelSkill = m_panelSkills[skill.GetId()];
        if (!panelSkill)
        {
            return;
        }
        panelSkill.UseSkill(skill.GetCooldown());
    }

    public void AddSkill(Skill skill)
    {
        PanelUISkill panelSkill = m_panelSkills[skill.GetId()];
        if (!panelSkill)
        {
            panelSkill = Instantiate(m_panelSkillUnit);
            m_panelSkills[skill.GetId()] = panelSkill;
            panelSkill.gameObject.transform.SetParent(transform);
            panelSkill.SetImageMaterial(skill.GetIconMaterial());
            panelSkill.transform.localScale = Vector3.one;
            panelSkill.SetSkillActive(skill.IsActive());
        }
        panelSkill.SetLevel(skill.GetLevel());
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        m_panelSkills = new PanelUISkill[(int)Skill.SkillType.Count];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
