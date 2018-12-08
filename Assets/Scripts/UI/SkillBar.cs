using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour {

    [SerializeField] private PlayerSkills m_playerSkills;
    [SerializeField] private Button m_selectButton;
    [SerializeField] private Text m_countOfSkillsText;

    private Skill m_currentSkill;
    private GameObject m_myEventSystem;

    public void SelectSkill(Skill skill)
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

    public void ApplySkill()
    {
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
        m_playerSkills.AddSkill(m_currentSkill);
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
}
