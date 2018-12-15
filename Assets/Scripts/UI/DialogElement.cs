using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogElement : MonoBehaviour {

    [SerializeField] public string m_replica;
    [SerializeField] public bool m_isPlayer;
    [SerializeField] public float m_lifeTime;
    [SerializeField] public List<DialogManager.CondAndReplica> m_nextVariants;
    [SerializeField] public List<Switcher> m_switcher;

    public DialogElement GetNextVariant(GameObject player)
    {
        if (m_nextVariants.Count == 0)
        {
            return null;
        }

        foreach (var variant in m_nextVariants)
        {
            if (variant.condition == null
                || variant.condition.ProcessPlayer(player))
            {
                return variant.variant;
            }
        }

        return null;
    }

    public void SwitchAll()
    {
        m_switcher.ForEach((Switcher s) => { s.Switch(); });
    }
}
