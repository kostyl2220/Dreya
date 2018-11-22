using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleBrainState : MonoBehaviour
{
    static protected float EPSILON = 0.000001f;

    public abstract string GetStateName();

    protected SimpleBrain m_parent;

    //instead of Update use this
    public abstract bool UpdateState();

    //Runs every time, when you gets into this node
    public abstract void Setup();

    //Runs only once, when stage is loaded
    protected abstract void Finalized();

    private void Start()
    {
        m_parent = GetComponent<SimpleBrain>();
        Finalized();
    }

    protected bool SetNewState(SimpleBrainState state)
    {
        m_parent.SetState(state ? state : this);
        return state;
    }
}
