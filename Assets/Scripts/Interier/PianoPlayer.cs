using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlayer : Interactable {

    [SerializeField] private List<AudioClip> m_sounds;
    [SerializeField] private float m_cooldown;

    private float m_lastPlayedTime;
    private int m_playSoundID;

    protected override void InteractWithPlayer(GameObject player)
    {
        if (m_lastPlayedTime + m_cooldown < Time.time)
        {
            m_playSoundID = 0;
        }
        else
        {
            m_playSoundID = (m_playSoundID + 1) % m_sounds.Count;
        }
        AudioSource.PlayClipAtPoint(m_sounds[m_playSoundID], gameObject.transform.position);
        m_lastPlayedTime = Time.time;
        Debug.Log("Play sound!");
    }

    // Use this for initialization
    void Start () {
        m_playSoundID = 0;
        m_lastPlayedTime = 0;
    }

}
