using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSequenceChecker : Interactable {
    [SerializeField] private RightSequenceEnter m_sequence;

    protected override void InteractWithPlayer(GameObject player)
    {
        m_sequence.CheckRightSequence();
    }
}
