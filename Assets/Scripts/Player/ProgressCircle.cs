using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCircle : MonoBehaviour {

    [SerializeField] private GameObject m_circle;

    public delegate void OnComplete();

    private float m_fillTime;
    private Vector3 m_currentScale;
    private OnComplete m_complete;

    public void SetProgress(float progressTime, OnComplete responce)
    {
        m_fillTime = progressTime;
        m_currentScale = Vector3.zero;
        m_circle.transform.localScale = m_currentScale;
        m_complete = responce;
        if (m_circle)
        {
            m_circle.SetActive(true);
        }
    }

    public void ResetProgress()
    {
        if (m_circle)
        {
            m_circle.SetActive(false);
        }
        m_complete = null;
    }

    private void Start()
    {
        if (m_circle)
        {
            m_circle.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (m_fillTime > 0.0f)
        {
            float alpha = Mathf.Min(Time.deltaTime / m_fillTime, 1.0f);
            m_currentScale = Vector3.Lerp(m_currentScale, Vector3.one, alpha);
            m_circle.transform.localScale = m_currentScale;
            m_fillTime -= Time.deltaTime;

            if (m_fillTime < 0.0f && m_complete != null)
            {
                m_circle.SetActive(false);
                m_complete.Invoke();
            }
        }
    }
}
