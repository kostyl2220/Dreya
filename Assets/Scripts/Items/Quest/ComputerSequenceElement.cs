using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSequenceElement : SequenceElement
{
    [SerializeField] private Light m_light;

    public override void Turn(bool enable)
    {
        m_light.enabled = enable;
    }
}
