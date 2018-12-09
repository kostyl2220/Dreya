using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour {

    [SerializeField] private PlayerSkills m_playerSkills;
    [SerializeField] private Button m_selectButton;
    [SerializeField] private Text m_countOfSkillsText;

    private UISkill m_currentSkill;
    private GameObject m_myEventSystem;

    public void SelectSkill(UISkill skill)
    {
        m_currentSkill = skill;
    }

    public void SetAvaliableSkills(int count)
    {
        if (m_countOfSkillsText)
        {
            m_countOfSkillsText.text = count.ToString();
        }

        if (m_selectButton)
        {
            m_selectButton.interactable = (count > 0);
        }
    }

    public int GetInitSkillLevel(Skill.SkillType skillType)
    {
        if (!m_playerSkills)
        {
            return 0;
        }

        Skill skill = m_playerSkills.GetSkill(skillType);

        if (skill == null)
        {
            return 0;
        }

        return skill.GetLevel();
    }

    public void ApplySkill()
    {
        if (!m_currentSkill)
        {
            return;
        }

        if (!m_playerSkills)
        {
            return;
        }

        if (m_playerSkills.m_avaliableSkills == 0)
        {
            return;
        }
        --m_playerSkills.m_avaliableSkills;

        //Apply skill to player
        Skill newSkill = Instantiate(m_currentSkill.GetSkill());
        int currentLevel = m_playerSkills.AddSkill(newSkill);
        m_currentSkill.SetSkillLevel(currentLevel);
        SetAvaliableSkills(m_playerSkills.m_avaliableSkills);
        ResetCurrentSkill();
    }

    private void ResetCurrentSkill()
    {
        if (!m_currentSkill)
        {
            return;
        }

        m_currentSkill = null;
        m_myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

	// Use this for initialization
	void Awake()
    {
        m_myEventSystem = GameObject.Find("EventSystem");
        if (!m_playerSkills)
        {
            m_playerSkills = GameObject.Find("Player").GetComponent<PlayerSkills>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Start()
    {

    }

}
