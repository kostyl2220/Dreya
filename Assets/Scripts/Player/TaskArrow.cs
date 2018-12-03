using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskArrow : MonoBehaviour {


    private TaskManager.TaskInfo m_taskInfo;
    // Use this for initialization
	void Start () {
        gameObject.SetActive(m_taskInfo != null);
    }

    public void SetNewTask(TaskManager.TaskInfo taskInfo)
    {
        m_taskInfo = taskInfo;
        gameObject.SetActive(m_taskInfo != null);
    }
	
	// Update is called once per frame
	void Update () {
		if (m_taskInfo == null)
        {
            return;
        }

        Vector3 taskDirection = m_taskInfo.m_pos.position - transform.position;
        taskDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(taskDirection.normalized, Vector3.up);
	}
}
