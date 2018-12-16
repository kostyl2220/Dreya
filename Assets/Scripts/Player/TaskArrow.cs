using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskArrow : MonoBehaviour {


    private float m_initHeight; // light Hack
    private TaskManager.TaskInfo m_taskInfo;
    // Use this for initialization
	void Start () {
        gameObject.SetActive(m_taskInfo != null);
        m_initHeight = gameObject.transform.position[1];
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

        transform.position = new Vector3(transform.position[0], m_initHeight, transform.position[2]);
        Vector3 taskDirection = m_taskInfo.m_pos.position - transform.position;
        taskDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(taskDirection.normalized, Vector3.up);
	}
}
