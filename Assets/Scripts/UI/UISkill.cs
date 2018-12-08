using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkill : MonoBehaviour {

    [SerializeField] private Skill m_skill;
    [SerializeField] private SkillBar m_skillBar;

    public void Enter()
    {
        if (!m_skill)
        {
            return;
        }

        Skill newSkill = Instantiate(m_skill);
        m_skillBar.SelectSkill(newSkill);
    }

	// Use this for initialization
	void Start () {
	    if (!m_skillBar)
        {
            m_skillBar = gameObject.transform.parent.transform.parent.GetComponent<SkillBar>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
