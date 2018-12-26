using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    [SerializeField] private TaskBar m_taskBar;
    [SerializeField] private List<TaskInfo> m_tasks;
    [SerializeField] private PlayerTaskManager m_ptm;

    private List<int> m_activeTasks;
    private List<int> m_completedTask;

    [System.Serializable]
    public class TaskInfo
    {
        [SerializeField] public string m_taskDescription;
        [SerializeField] public bool m_isMain;
        [SerializeField] public Transform m_pos;

        public int m_taskId { get; set; }

        public TaskInfo(string t, int i)
        {
            m_taskDescription = t;
            m_taskId = i;
        }

        public void SetId(int id)
        {
            m_taskId = id;
        }
    }

    public void SetupTasks()
    {
        // Code to remove
        if (m_taskBar && m_tasks.Count >= 2)
        {
            AddTask(0);
        }
    }

	// Use this for initialization
	void Start ()
    {
        m_completedTask = new List<int>();
        m_activeTasks = new List<int>();
        for (int i = 0; i < m_tasks.Count; ++i)
        {
            m_tasks[i].SetId(i);
        }
    }

    public bool IsTaskActive(int taskId)
    {
        return m_activeTasks.Contains(taskId) || m_completedTask.Contains(taskId);
    }

    public void AddTask(int taskId)
    {
        m_activeTasks.Add(taskId);
        TaskInfo taskInfo = m_tasks[taskId];
        m_taskBar.AddTask(taskInfo);
        m_ptm.SetTask(taskInfo);
    }

    public void RemoveTask(int taskId)
    {
        m_activeTasks.Remove(taskId);
        TaskInfo info = m_tasks[taskId];
        m_taskBar.CompleteTask(info.m_taskId, info.m_isMain);
        m_ptm.ResetTask(info.m_isMain);
        m_completedTask.Add(taskId);
    }
}
