using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    [SerializeField] private TaskBar m_taskBar;
    [SerializeField] private List<TaskInfo> m_tasks;
    [SerializeField] private PlayerTaskManager m_ptm;

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

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < m_tasks.Count; ++i)
        {
            m_tasks[i].SetId(i);
        }

        // Code to remove
        if (m_taskBar && m_tasks.Count >= 2)
        {
            AddTask(m_tasks[1]);
            AddTask(m_tasks[0]);
        }
    }

    void AddTask(TaskInfo taskInfo)
    {
        m_taskBar.AddTask(taskInfo);
        m_ptm.SetTask(taskInfo);
    }

    void RemoveTask(int taskId)
    {
        TaskInfo info = m_tasks[taskId];
        m_taskBar.CompleteTask(info.m_taskId, info.m_isMain);
        m_ptm.ResetTask(info.m_isMain);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
