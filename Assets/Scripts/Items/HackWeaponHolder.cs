using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackWeaponHolder : MonoBehaviour {
    [SerializeField] private Weapon m_weapon;

    public Weapon GetWeapon()
    {
        return m_weapon;
    }
}
