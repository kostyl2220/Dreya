using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSequenceEnter : MonoBehaviour {

    [SerializeField] private List<SequenceElement> m_listOfObjects;
    [SerializeField] private List<int> m_rightSequence;
    [SerializeField] private List<Switcher> m_itemsToSwitch;

    private int[] m_currentPressed;
    private int m_countOfPressed;
    private bool m_used;

    public void PressItem(int id)
    {
        if (m_used)
        {
            return;
        }

        m_listOfObjects[id].Turn(true);
        m_currentPressed[m_countOfPressed] = id;
        ++m_countOfPressed;
        if (m_countOfPressed == m_rightSequence.Count)
        {
            CheckRightSequence();
        }
    }

    private void CheckRightSequence()
    {
        for (int i = 0; i < m_rightSequence.Count; ++i)
        {
            if (m_currentPressed[i] != m_rightSequence[i])
            {
                OnFail();
                return;
            }
        }

        OnSuccess();
    }

    private void OnSuccess()
    {
        m_used = true;
        m_itemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
    }

    private void OnFail()
    {
        m_countOfPressed = 0;
        m_listOfObjects.ForEach((SequenceElement se) => { se.Turn(false); });
    }

	// Use this for initialization
	void Start () {
        m_currentPressed = new int[m_rightSequence.Count];
        m_countOfPressed = 0;
        m_used = false;
        for (int i = 0; i < m_listOfObjects.Count; ++i)
        {
            m_listOfObjects[i].SetId(i);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
