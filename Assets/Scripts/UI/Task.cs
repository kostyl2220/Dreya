using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

    [SerializeField] private Text m_text;

    private int m_taskId;
    public void SetTask(TaskManager.TaskInfo taskInfo)
    {
        if (m_text)
        {
            m_text.text = taskInfo.m_taskDescription;
        }
        m_taskId = taskInfo.m_taskId;
    }

    public int GetTaskId()
    {
        return m_taskId;
    }

    public void OnTaskDone()
    {
        //TODO
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
