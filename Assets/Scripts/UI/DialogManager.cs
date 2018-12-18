using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField] private DialogTextSetter m_playerDialog;
    [SerializeField] private DialogTextSetter m_otherAnswer;
    [SerializeField] private GameObject m_player;

    public abstract class ReplicaCondition : MonoBehaviour
    {
        public abstract bool ProcessPlayer(GameObject player);
    }

    [System.Serializable]
    public struct CondAndReplica
    {
        [SerializeField] public ReplicaCondition condition;
        [SerializeField] public DialogElement variant;
    }

    private bool m_waitForResponce;
    private float m_nextReplicaTime;
    private DialogElement m_dialogStart;

    public void SetDialog(DialogElement dialog)
    {
        m_dialogStart = dialog;
        if (m_dialogStart.m_lifeTime <= 0.0f)
        {
            LockPlayer(true);
        }
        PlayCurrentReplica();
    }

    private void LockPlayer(bool setLock)
    {
        SimpleCharacterControl scc = m_player.GetComponent<SimpleCharacterControl>();
        if (scc)
        {
            scc.SetDisabledMovement(setLock);
        }
    }

    public void SetEnemyFollower(Transform transform)
    {
        if (m_otherAnswer)
        {
            ObjectFollower of = m_otherAnswer.gameObject.GetComponent<ObjectFollower>();
            if (of)
            {
                of.SetTarget(transform);
            }
        }
    }

    private void PlayCurrentReplica()
    {
        if (m_dialogStart == null)
        {
            m_playerDialog.gameObject.SetActive(false);
            m_otherAnswer.gameObject.SetActive(false);
            return;
        }

        DialogTextSetter enable = m_dialogStart.m_isPlayer ? m_playerDialog : m_otherAnswer;
        DialogTextSetter disable = m_dialogStart.m_isPlayer ? m_otherAnswer : m_playerDialog;
        enable.gameObject.SetActive(true);
        disable.gameObject.SetActive(false);
        enable.SetText(m_dialogStart.m_replica);

        if (m_dialogStart.m_lifeTime > 0.0f)
        {
            m_nextReplicaTime = Time.time + m_dialogStart.m_lifeTime;
        }
    }

    private void ShowNextReplica()
    {
        m_dialogStart.SwitchAll();
        DialogElement nextVar = m_dialogStart.GetNextVariant(m_player);
        if (nextVar == null)
        {
            if (m_dialogStart.m_lifeTime <= 0.0f)
            {
                LockPlayer(false);
            }
            m_dialogStart = null;
            return;
        }
        m_dialogStart = nextVar;
    }

	// Update is called once per frame
	void Update ()
    {
		if (m_dialogStart == null)
        {
            return;
        }

        if (m_dialogStart.m_lifeTime > 0.0f)
        {
            if (Time.time < m_nextReplicaTime)
            {
                return;
            }

            ShowNextReplica();
            PlayCurrentReplica();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextReplica();
            PlayCurrentReplica();
        }
	}
}
