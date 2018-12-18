using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTaskManager : MonoBehaviour {

    [SerializeField] private TaskArrow m_mainArrow;
    [SerializeField] private TaskArrow m_auxiliaryArrow;

    public void SetTask(TaskManager.TaskInfo taskInfo)
    {
        TaskArrow arrow = (taskInfo.m_isMain ? m_mainArrow : m_auxiliaryArrow);
        arrow.SetNewTask(taskInfo);
    }

    public void ResetTask(bool isMain)
    {
        TaskArrow arrow = (isMain ? m_mainArrow : m_auxiliaryArrow);
        arrow.SetNewTask(null);
    }
}
