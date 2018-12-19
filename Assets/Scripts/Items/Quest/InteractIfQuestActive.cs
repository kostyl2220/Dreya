using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractIfQuestActive : Interactable {
    [SerializeField] private int m_taskId;
    [SerializeField] private TaskManager m_taskManager;

    protected override bool CanInteractWithPlayer()
    {
        if (!m_taskManager)
        {
            return false;
        }

        return m_taskManager.IsTaskActive(m_taskId);
    }

    // Use this for initialization
    protected new void Start () {
        base.Start();

        if (!m_taskManager)
        {
            m_taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        }
	}
}
