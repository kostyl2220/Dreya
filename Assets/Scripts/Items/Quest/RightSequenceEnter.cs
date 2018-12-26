using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightSequenceEnter : Interactable
{
    static int INVALID_ID = -1;

    [SerializeField] private List<SequenceElement> m_listOfObjects;
    [SerializeField] private List<int> m_rightSequence;
    [SerializeField] private List<Switcher> m_addedItemSwithers;
    [SerializeField] private List<Switcher> m_itemsToSwitch;
    [SerializeField] private List<Switcher> m_loseItemsToSwitch;
    [SerializeField] private bool m_checkAfterAdding = true;
    [SerializeField] private bool m_sequential = false;

    protected int m_selectedItem;
    protected List<int> m_currentPressed;
    protected bool m_used;

    public void PickItem(int id)
    {
        if (m_selectedItem != INVALID_ID)
        {
            m_listOfObjects[m_selectedItem].Turn(false);
        }
        m_selectedItem = id;
        m_listOfObjects[id].Turn(true);
    }

    public void PressItem(int id)
    {
        if (m_used)
        {
            return;
        }

        if (m_sequential && id == m_rightSequence[0])
        {
            m_currentPressed.Clear();
        }

        m_listOfObjects[id].Turn(true);
        m_currentPressed.Add(id);
        if (m_checkAfterAdding && m_currentPressed.Count == m_rightSequence.Count)
        {
            CheckRightSequence();
        }
        else
        {
            m_addedItemSwithers.ForEach((Switcher s) => { s.Switch(); });
        }
    }

    public void CheckRightSequence()
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
        m_currentPressed.Clear();
        m_loseItemsToSwitch.ForEach((Switcher s) => { s.Switch(); });
        m_listOfObjects.ForEach((SequenceElement se) => { se.Turn(false); });
    }

    // Use this for initialization
    protected new void Start ()
    {
        base.Start();
        m_currentPressed = new List<int>(m_rightSequence.Count);
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
