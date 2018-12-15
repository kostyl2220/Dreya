using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fearable : MonoBehaviour {

    //TODO separate to special HUD class
    [SerializeField] private float m_initFear = 0.0f;
    [SerializeField] private float m_maxFear;
    [SerializeField] private Image m_fearBar;
    [SerializeField] private Text m_fearText;

    public delegate void PlayerScaredEvent(float coef);
    public event PlayerScaredEvent OnScared;

    private float m_currentFear;
    private float m_fearBarHeight;
    private PlayerSkills m_playerSkills;

    public void ChangeFear(float fear)
    {
        m_currentFear = Mathf.Clamp(GetFear() + fear, 0, GetMaxFear());
        UpdateHUD();

        if (OnScared != null)
        {
            OnScared.Invoke(GetFearPersentage());
        }
    }

    public void ResetFear()
    {
        m_currentFear = 0.0f;
    }

    private void UpdateHUD()
    {
        if (m_fearText)
        {
            m_fearText.text = GetFear() + "/" + GetMaxFear();
        }
        if (m_fearBar)
        {
            m_fearBar.rectTransform.sizeDelta = new Vector2(m_fearBar.rectTransform.sizeDelta.x, m_fearBarHeight * GetFearPersentage());
        }
    }
    
    public float GetFearPersentage()
    {
        return GetFear() / GetMaxFear();
    }

    public float GetFear()
    {
        return m_currentFear;
    }

    public float GetMaxFear()
    {
        Skill skill = m_playerSkills.GetSkill(Skill.SkillType.AdditionalHP);
        return m_maxFear + (skill == null ? 0 : skill.GetEffect());
    }

    // Use this for initialization
    void Start () {
        m_currentFear = m_initFear;	
        if (m_fearBar)
        {
            m_fearBarHeight = m_fearBar.rectTransform.sizeDelta.y; ;
        }
        if (!m_playerSkills)
        {
            m_playerSkills = GetComponent<PlayerSkills>();
        }
        if (m_playerSkills)
        {
            m_playerSkills.m_onAddSkill += (() => { UpdateHUD(); });
        }
        UpdateHUD();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
