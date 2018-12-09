using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUISkill : MonoBehaviour {

    [SerializeField] private Text m_skillLevel;
    [SerializeField] private Image m_fillFrame;

    private float m_fillPercentage;
    private float m_reloadTimeLeft = -1.0f;
	
    public void SetImageMaterial(Material material)
    {
        Image image = GetComponent<Image>();
        if (image)
        {
            image.material = material;
        }
    }

    public void SetSkillActive(bool active)
    {
        if (m_fillFrame)
        {
            m_fillFrame.gameObject.SetActive(active);
        }
    }

    public void SetLevel(int level)
    {
        if (m_skillLevel)
        {
            m_skillLevel.text = level.ToString();
        }
    }

    public void UseSkill(float reloadTime)
    {
        m_reloadTimeLeft = reloadTime;
        m_fillPercentage = 0.0f;
    }

    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		if (m_reloadTimeLeft < 0.0f)
        {
            return;
        }

        if (!m_fillFrame)
        {
            return;
        }

        float alpha = Mathf.Min(Time.deltaTime / m_reloadTimeLeft, 1.0f);
        m_fillPercentage = Mathf.Lerp(m_fillPercentage, 1.0f, alpha);
        m_fillFrame.fillAmount = m_fillPercentage;
        m_reloadTimeLeft -= Time.deltaTime;
    }
}
