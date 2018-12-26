using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject m_HUD;
    [SerializeField] private Switcher m_switch;

    private string m_suisideCommand = "suiside";
    private string m_currentCommand;

    private bool m_enablePrint = false;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
        m_currentCommand = "";
    }

    public void ResetFocus()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void StartGame()
    {
        m_switch.SwitchOff();
        if (m_HUD)
        {
            m_HUD.SetActive(true);
        }

        Time.timeScale = 1.0f;
    }

    public void ReloadGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_enablePrint)
        {
            for (int i = 0; i < 27; ++i)
            {
                if (Input.GetKeyUp((KeyCode)((int)KeyCode.A + i)))
                {
                    char currentLetter = (char)(KeyCode.A + i);
                    Debug.Log(currentLetter);
                    m_currentCommand += currentLetter;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_enablePrint = !m_enablePrint;

            if (m_enablePrint)
            {
                return;
            }

            if (m_currentCommand == m_suisideCommand)
            {
                m_currentCommand = "";
                ReloadGame();
            }
            m_currentCommand = "";
        }
	}
}
