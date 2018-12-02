using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour {

    [SerializeField] private Task m_newTask;
    [SerializeField] private LayoutGroup m_mainTaskBar;
    [SerializeField] private LayoutGroup m_auxiliaryTaskBar;
    [SerializeField] private Text m_mainTasksText;
    [SerializeField] private Text m_auxiliaryTasksText;

    private List<Task> m_mainTasks;
    private List<Task> m_auxiliaryTasks;

    void OnEnable () {
        m_mainTasks = new List<Task>();
        m_auxiliaryTasks = new List<Task>();
	}

    private void CheckTaskPresence()
    {
        m_mainTasksText.gameObject.SetActive(m_mainTasks.Count > 0);
        m_auxiliaryTasksText.gameObject.SetActive(m_auxiliaryTasks.Count > 0);
        m_mainTaskBar.gameObject.SetActive(m_mainTasks.Count > 0);
        m_auxiliaryTaskBar.gameObject.SetActive(m_auxiliaryTasks.Count > 0);
    }

    public void AddTask(TaskManager.TaskInfo taskInfo, bool isMain)
    {
        List<Task> listAddTo = isMain ? m_mainTasks : m_auxiliaryTasks;
        LayoutGroup parentLayout = isMain ? m_mainTaskBar : m_auxiliaryTaskBar;
        Task newTask = Instantiate(m_newTask);
        newTask.SetTask(taskInfo);
        newTask.transform.SetParent(parentLayout.transform);
        listAddTo.Add(newTask);
        CheckTaskPresence();
    }

    public void CompleteTask(int taskId, bool isMain)
    {
        List<Task> listToSearchIn = isMain ? m_mainTasks : m_auxiliaryTasks;
        int completedTaskInListId = listToSearchIn.FindIndex((Task t) => { return t.GetTaskId() == taskId; });
        if (completedTaskInListId != -1)
        {
            Task completedTask = listToSearchIn[completedTaskInListId];
            listToSearchIn.RemoveAt(completedTaskInListId);
            Destroy(completedTask.gameObject);
            CheckTaskPresence();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
