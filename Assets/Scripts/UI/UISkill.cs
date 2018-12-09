using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour {

    [SerializeField] private Skill m_skill;
    [SerializeField] private SkillBar m_skillBar;
    [SerializeField] private Text m_skillLevel;

    public void Enter()
    {
        if (!m_skill)
        {
            return;
        }

        m_skillBar.SelectSkill(this);
    }

    public Skill GetSkill()
    {
        return m_skill;
    }

    public void SetSkillLevel(int level)
    {
        if (m_skillLevel)
        {
            m_skillLevel.text = level.ToString();
        }
    }

	// Use this for initialization
	void Start () {
	    if (!m_skillBar)
        {
            m_skillBar = gameObject.transform.parent.transform.parent.GetComponent<SkillBar>();
        }
        if (m_skillBar)
        {
            SetSkillLevel(m_skillBar.GetInitSkillLevel(m_skill.GetSkillType()));
        }

        Image image = GetComponent<Image>();
        if (image)
        {
            image.material = m_skill.GetIconMaterial();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
