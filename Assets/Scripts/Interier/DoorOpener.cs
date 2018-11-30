using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : Switcher {

    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_parameterName = "open";
    [SerializeField] private GameObject m_player;
    [SerializeField] private float m_maxRotateAngle = 100.0f;
    [SerializeField] private float m_doorAngleSpeed = 100.0f;
    [SerializeField] private float m_doorSinglePush = 3.0f;
    [SerializeField] private Vector3 m_clockwiseDirection = new Vector3(1.0f, 0.0f, 0.0f);

    private float m_doorRotateAngle;
    private float m_endAngle;
    private float m_rotationTimeLeft;
    private float m_doorOpenSide;
	// Use this for initialization
	void Start () {
		if (!m_animator)
        {
            m_animator = GetComponent<Animator>();
        }

        m_doorRotateAngle = (m_switchValue ? m_maxRotateAngle : 0.0f);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_doorRotateAngle);
	}

    void MoveDoor(bool clockwise)
    {
        m_doorOpenSide = (clockwise ? 1.0f : -1.0f);
        m_endAngle = (m_doorRotateAngle * m_doorOpenSide < 0.0f) ? 0.0f : m_maxRotateAngle * m_doorOpenSide;
        float angleDiff = Mathf.Abs(m_doorRotateAngle - m_endAngle);
        m_rotationTimeLeft = angleDiff / m_doorAngleSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        bool clockwiseDirection = InClockwise(collision.transform);
        m_doorOpenSide = (clockwiseDirection ? 1.0f : -1.0f);
        m_endAngle = m_doorRotateAngle + m_doorSinglePush * m_doorOpenSide;
        m_rotationTimeLeft = m_doorSinglePush / m_doorAngleSpeed;
    }

    private bool InClockwise(Transform transform)
    {
        return Vector3.Angle(transform.forward, m_clockwiseDirection) < 90.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_rotationTimeLeft > 0.0f)
        {
            float alpha = Mathf.Min(1.0f, Time.deltaTime / m_rotationTimeLeft);
            m_doorRotateAngle = Mathf.Lerp(m_doorRotateAngle, m_endAngle, alpha);
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_doorRotateAngle);
            m_rotationTimeLeft -= Time.deltaTime;
        }
    }

    protected override void Switched()
    {
        /*if (m_animator)
        {
            m_animator.SetBool(m_parameterName, m_switchValue);
        }*/
        MoveDoor(InClockwise(m_player.transform));
    }
}
