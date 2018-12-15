using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedState : SimpleBrainState {

    public override string GetStateName()
    {
        return "LockedState";
    }

    public override void Setup()
    {
        
    }

    public override bool UpdateState()
    {
        return false;
    }

    protected override void Finalized()
    {
        
    }
}
