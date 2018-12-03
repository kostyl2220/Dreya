using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour {

    [System.Serializable]
    public struct TaskSet
    {
        [SerializeField] public LayoutGroup m_bar;
        [SerializeField] public Text m_text;

        public List<Task> m_list;
    }

    [SerializeField] private Task m_newTask;
    [SerializeField] private TaskSet m_mainSet;
    [SerializeField] private TaskSet m_auxiliarySet;

    private List<Task> m_auxiliaryTasks;

    void OnEnable () {
        m_mainSet.m_list = new List<Task>();
        m_auxiliarySet.m_list = new List<Task>();
	}

    private void CheckTaskPresence(bool isMain)
    {
        TaskSet set = isMain ? m_mainSet : m_auxiliarySet;
        set.m_text.gameObject.SetActive(set.m_list.Count > 0);
        set.m_bar.gameObject.SetActive(set.m_list.Count > 0);
    }

    public void AddTask(TaskManager.TaskInfo taskInfo)
    {
        TaskSet set = taskInfo.m_isMain ? m_mainSet : m_auxiliarySet;
        Task newTask = Instantiate(m_newTask);
        newTask.SetTask(taskInfo);
        newTask.transform.SetParent(set.m_bar.transform);
        set.m_list.Add(newTask);
        CheckTaskPresence(taskInfo.m_isMain);
    }

    public void CompleteTask(int taskId, bool isMain)
    {
        List<Task> listToSearchIn = isMain ? m_mainSet.m_list : m_auxiliarySet.m_list;
        int completedTaskInListId = listToSearchIn.FindIndex((Task t) => { return t.GetTaskId() == taskId; });
        if (completedTaskInListId != -1)
        {
            Task completedTask = listToSearchIn[completedTaskInListId];
            listToSearchIn.RemoveAt(completedTaskInListId);
            Destroy(completedTask.gameObject);
            CheckTaskPresence(isMain);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
