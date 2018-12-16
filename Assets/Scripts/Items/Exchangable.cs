using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchangable : Item {

    [SerializeField] private float m_timeToDestroy = 5.0f;
    [SerializeField] private float m_blinkingStartPersentage = 0.5f;
    [SerializeField] private int m_countOfBlinkings = 3;
    [SerializeField] private Renderer m_renderer;

    private float m_oneBlinkTime;
    private float m_leftTime;
    private float m_startBlinkTime;
    private bool m_blinking;
    private int m_blinksLeft;
  
    private Color m_startColor;
    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        if (!m_renderer)
        {
            m_renderer = gameObject.GetComponent<Renderer>();
        }
        m_startColor = m_renderer ? m_renderer.material.color: Color.white;
        m_oneBlinkTime = m_timeToDestroy * (1.0f - m_blinkingStartPersentage) / m_countOfBlinkings;
    }

    public void StartDestroying()
    {
        m_leftTime = m_oneBlinkTime;
        m_blinking = true;
        m_blinksLeft = m_countOfBlinkings;
        m_startBlinkTime = Time.time + m_timeToDestroy * m_blinkingStartPersentage;
    }

    public void StopDestroying()
    {
        m_blinking = false;
        SetTransparency(1.0f);
    }

    public override void Picked()
    {
        base.Picked();
        StopDestroying();
    }

    private void SetTransparency(float transparency)
    {
        if (m_renderer)
        {
            m_startColor.a = transparency;
            m_renderer.material.color = m_startColor;
        }
    }

    private void UpdateBlinking()
    {
        if (m_leftTime < 0.0f)
        {
            m_leftTime = m_oneBlinkTime;
            --m_blinksLeft;
            if (m_blinksLeft == 0)
            {
                Destroy(GetRoot());
                //destroy;
            }
            return;
        }

        float currentPersentage = 2.0f * m_leftTime / m_oneBlinkTime;
        float normalized = Mathf.Abs(currentPersentage - 1.0f);
        SetTransparency(normalized);
        m_leftTime -= Time.deltaTime;
    }

    // Update is called once per frame
    protected new void Update ()
    {
        base.Update();

        if (!m_blinking)
        {
            return;
        }

        if (Time.time < m_startBlinkTime)
        {
            return;
        }

        UpdateBlinking();
    }
}
