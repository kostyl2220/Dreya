using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : SimpleBrainState {
    [SerializeField] private float m_dyingTime = 1.0f;
    [SerializeField] private float m_dyingMinScale = 0.3f;
    [SerializeField] private GameObject m_afterDyingObject;

    private DamageReceiveComponent m_drc;
    private Vector3 m_currentScale;
    private Vector3 m_endVector;

    public override string GetStateName()
    {
        return "DyingState";
    }

    public override void Setup()
    {
        m_currentScale = Vector3.one;
        m_endVector = new Vector3(m_dyingMinScale, m_dyingMinScale, m_dyingMinScale);
    }

    public override bool UpdateState()
    {
        if (m_dyingTime < 0)
        {
            if (m_afterDyingObject)
            {
                Instantiate(m_afterDyingObject, m_parent.transform);
            }
            Destroy(gameObject);
            return true;
        }

        float alpha = Mathf.Min(Time.deltaTime / m_dyingTime, 1.0f);
        m_currentScale = Vector3.Lerp(m_currentScale, m_endVector, alpha);
        m_parent.transform.localScale = m_currentScale;

        m_dyingTime -= Time.deltaTime;
        return false;
    }

    protected override void Finalized()
    {
        m_drc = gameObject.GetComponent<DamageReceiveComponent>();
        if (m_drc)
        {
            m_drc.OnDeath += ((float damage, Vector3 pushForce) => { SetNewState(this); });
        }
    }
}
