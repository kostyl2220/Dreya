using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : DamageReceiveComponent {

    [SerializeField] private Image m_healthBar;

    private float m_healthBarWidth;

    public override bool GetDamage(float damage, Vector3 pushForce, GameObject attacker)
    {
        bool isAlive = base.GetDamage(damage, pushForce, attacker);

        UpdateHUD();

        return isAlive;
    }

    private void UpdateHUD()
    {
        if (m_healthBar)
        {
            m_healthBar.rectTransform.sizeDelta = new Vector2(m_healthBarWidth * GetHealthPersentage(), m_healthBar.rectTransform.sizeDelta.y);
        }
    }
    
    public float GetHealthPersentage()
    {
        return GetHP() / GetMaxHP();
    }

    protected new void Start ()
    {
        base.Start();
        if (m_healthBar)
        {
            m_healthBarWidth = m_healthBar.rectTransform.sizeDelta.x; ;
        }
        UpdateHUD();     
    }
}
