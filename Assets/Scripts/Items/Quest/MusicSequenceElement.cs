using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSequenceElement : SequenceElement {

    [SerializeField] private AudioClip m_audio;

    public override void Turn(bool enable)
    {
        if (enable)
        {
            AudioSource.PlayClipAtPoint(m_audio, gameObject.transform.position);
        }
    }
}
