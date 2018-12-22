using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistrySequenceElement : SequenceElement {

    public override void Turn(bool enable)
    {
        gameObject.SetActive(!enable);
    }
}
