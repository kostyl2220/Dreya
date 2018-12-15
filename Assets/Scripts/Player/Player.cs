using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private GameObject m_endWindow;
    [SerializeField] private Transform m_startTransorm;

    private Fearable m_fearable;
	// Use this for initialization
	void Start () {
        m_fearable = GetComponent<Fearable>();
        if (m_fearable)
        {
            m_fearable.OnScared += (float fear) => { ProcessFear(fear); };
        }
        SetupPlayer();
    }

    public void SetTransform(Transform transform)
    {
        m_startTransorm = transform;
    }

    private void ProcessFear(float percentage)
    {
        if (percentage >= 1.0f)
        {
            //player is dead, reload game.
            Time.timeScale = 0.0f;

            if (m_endWindow)
            {
                m_endWindow.SetActive(true);
            }
        }
    }

    private void SetupPlayer()
    {
        if (m_startTransorm)
        {
            gameObject.transform.position = m_startTransorm.position;
            gameObject.transform.rotation = m_startTransorm.rotation;
        }
        if (m_fearable)
        {
            m_fearable.ResetFear();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
