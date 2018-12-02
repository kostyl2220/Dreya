using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    [SerializeField] private TaskBar m_taskBar;

    public struct TaskInfo
    {
        public int m_taskId { get; set; }
        public string m_taskDescription { get; set; }

        public TaskInfo(string t, int i)
        {
            m_taskDescription = t;
            m_taskId = i;
        }
    }

	// Use this for initialization
	void Start () {
		if (m_taskBar)
        {
            m_taskBar.AddTask(new TaskInfo("Your main task is to survive", 0), true);
            m_taskBar.AddTask(new TaskInfo("Your auxiliary task is to make fun", 1), false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
