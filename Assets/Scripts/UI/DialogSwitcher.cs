using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSwitcher : Switcher {

    [SerializeField] private DialogManager m_manager;
    [SerializeField] private DialogElement m_dialog;
    [SerializeField] private Transform m_enemyToTalk;

    protected override void Switched()
    {
        if (m_switchValue && m_dialog && m_manager)
        {
            m_manager.SetEnemyFollower(m_enemyToTalk);
            m_manager.SetDialog(m_dialog);
        }
    }
}
