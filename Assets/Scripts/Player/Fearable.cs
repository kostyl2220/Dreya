using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fearable : MonoBehaviour {

    [SerializeField] private float m_initFear = 0.0f;
    [SerializeField] private float m_maxFear;
    [SerializeField] private Image m_fearBar;
    [SerializeField] private Text m_fearText;

    private float m_currentFear;
    private float m_fearBarHeight;

    public void ChangeFear(float fear)
    {
        m_currentFear = Mathf.Clamp(m_currentFear + fear, 0, m_maxFear);
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (m_fearText)
        {
            m_fearText.text = m_currentFear + "/" + m_maxFear;
        }
        if (m_fearBar)
        {
            m_fearBar.rectTransform.sizeDelta = new Vector2(m_fearBar.rectTransform.sizeDelta.x, m_fearBarHeight * GetFearPersentage());
        }
    }
    
    public float GetFearPersentage()
    {
        return m_currentFear / m_maxFear;
    }

    public float GetFear()
    {
        return m_currentFear;
    }

    // Use this for initialization
    void Start () {
        m_currentFear = m_initFear;	
        if (m_fearBar)
        {
            m_fearBarHeight = m_fearBar.rectTransform.sizeDelta.y; ;
        }
        UpdateHUD();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
