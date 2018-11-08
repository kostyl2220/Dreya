using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleBrainState : MonoBehaviour
{
    protected SimpleBrain m_parent;
    public abstract bool UpdateState();

    //Runs every time, when you gets into this node
    protected abstract void Setup();

    //Runs only once, when stage is loaded
    protected abstract void Finalized();

    private void Start()
    {
        m_parent = GetComponent<SimpleBrain>();
        Finalized();
    }

    protected void SetNewState(SimpleBrainState state)
    {
        state.Setup();
        m_parent.SetState(state);
    }
}
