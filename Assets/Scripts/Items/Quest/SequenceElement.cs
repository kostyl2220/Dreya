using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceElement : Interactable {

    [SerializeField] private RightSequenceEnter m_sequence;

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
            m_sequence.PressItem(m_innerId);
        }
    }
}
