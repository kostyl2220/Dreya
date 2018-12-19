using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceElement : Interactable {

    [SerializeField] private RightSequenceEnter m_sequence;
    [SerializeField] private bool m_onlyPick = false;

    private int m_innerId;

    public void SetId(int id)
    {
        m_innerId = id;
    }

    public virtual void Turn(bool enable)
    {

    }

    protected override void InteractWithPlayer(GameObject player)
    {
        if (m_sequence)
        {
            if (m_onlyPick)
            {
                m_sequence.PickItem(m_innerId);
            }
            else
            {
                m_sequence.PressItem(m_innerId);
            }         
        }
    }
}
