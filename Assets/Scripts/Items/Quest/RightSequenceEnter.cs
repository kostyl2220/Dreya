using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightSequenceEnter : Interactable
{
    static int INVALID_ID = -1;

    [SerializeField] private List<SequenceElement> m_listOfObjects;
    [SerializeField] private List<int> m_rightSequence;
    [SerializeField] private List<Switcher> m_itemsToSwitch;

    protected int m_selectedItem;
    protected int[] m_currentPressed;
    protected int m_countOfPressed;
    protected bool m_used;

    public void PickItem(int id)
    {
        m_selectedItem = id;
    }

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
    protected new void Start ()
    {
        base.Start();
        m_currentPressed = new int[m_rightSequence.Count];
        m_countOfPressed = 0;
        m_used = false;
        for (int i = 0; i < m_listOfObjects.Count; ++i)
        {
            m_listOfObjects[i].SetId(i);
        }
        m_selectedItem = INVALID_ID;
    }

    protected override void InteractWithPlayer(GameObject player)
    {
        if (m_selectedItem == INVALID_ID)
        {
            return;
        }

        PressItem(m_selectedItem);
        m_selectedItem = INVALID_ID;
    }
}
