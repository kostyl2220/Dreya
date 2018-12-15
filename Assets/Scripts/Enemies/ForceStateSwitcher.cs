using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceStateSwitcher : Switcher {

    [SerializeField] private SimpleBrainState m_nextState;

    protected override void Switched()
    {
        if (!m_switchValue)
        {
            return;
        }

        SimpleBrain sb = GetComponent<SimpleBrain>();
        if (sb)
        {
            sb.SetState(m_nextState);
        }
    }
}
