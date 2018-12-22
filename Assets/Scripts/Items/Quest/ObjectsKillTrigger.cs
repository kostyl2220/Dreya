using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsKillTrigger : MonoBehaviour {
    [SerializeField] private List<DamageReceiveComponent> m_objectsToKill;
    [SerializeField] private List<Switcher> m_shitchers;

    private int m_countLeft;

	// Use this for initialization
	void Start () {
        m_countLeft = m_objectsToKill.Count;
        m_objectsToKill.ForEach((DamageReceiveComponent drc) => { drc.OnDeath += OnDied; });
	}

    private void OnDied()
    {
        --m_countLeft;
        if (m_countLeft == 0)
        {
            m_shitchers.ForEach((Switcher s) => { s.Switch(); }); 
        }
    }
}
