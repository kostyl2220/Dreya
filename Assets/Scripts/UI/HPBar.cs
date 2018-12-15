using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private RectTransform m_HPbar;
    [SerializeField] private Text m_HPtext;
    [SerializeField] private DamageReceiveComponent m_enemy;

    private Canvas Can;
    //HP bar
    private float totalHPbarWidth;

    // Use this for initialization
    void Start()
    {
        if (m_HPbar)
        {
            totalHPbarWidth = m_HPbar.sizeDelta.x;
        }

        gameObject.SetActive(false);
        if (m_enemy)
        {
            DrawHP(m_enemy.GetHP(), m_enemy.GetMaxHP());
            m_enemy.OnHit += (float damage, Vector3 pushForce) => { DrawHP(m_enemy.GetHP(), m_enemy.GetMaxHP()); };
        }
        Can = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = transform.position - Camera.main.transform.position;
        Direction.x = 0;
        Quaternion rotation = Quaternion.LookRotation(Direction);
        transform.rotation = rotation;
    }

    void DrawHP(double HP, double fullHP)
    {
        gameObject.SetActive(true);
        if (m_HPbar)
        {
            m_HPbar.sizeDelta = new Vector2((float)(HP / fullHP * totalHPbarWidth), m_HPbar.sizeDelta.y);
        }
        if (m_HPtext)
            m_HPtext.text = string.Format("{0}/{1}", HP, fullHP);
    }
}
