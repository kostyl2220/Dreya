using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintAndQuest : Switcher
{
    static int INVALID_TASK_ID = -1;

    [SerializeField] private int m_completedTask = INVALID_TASK_ID;
    [SerializeField] private int m_nextTask = INVALID_TASK_ID;
    [SerializeField] private DialogElement m_dialogToPlay;
    [SerializeField] private DialogManager m_dialogManager;
    [SerializeField] private TaskManager m_taskManager;

    private bool m_taskIsGiven;

    protected override void Switched()
    {

        if (m_dialogManager && m_dialogToPlay)
        {
            m_dialogManager.SetDialog(m_dialogToPlay);
        }

        if (!m_taskIsGiven && m_taskManager && m_completedTask != INVALID_TASK_ID)
        {
            m_taskIsGiven = true;
            m_taskManager.RemoveTask(m_completedTask);
            m_taskManager.AddTask(m_nextTask);
        }
    }

    protected new void Start ()
    {
        m_taskIsGiven = false;
        if (!m_dialogManager)
        {
            m_dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        }
        if (!m_taskManager)
        {
            m_taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        }
	}
}
