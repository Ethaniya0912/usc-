using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] MeleeDamageCollider meleeDamageCollider;

        private void Awake()
        {
            meleeDamageCollider = GetComponentInChildren<MeleeDamageCollider>();
        }

        public void SetWeaponDamage(CharacterBase characterWieldingWeapon, WeaponItem weapon)
        {
            meleeDamageCollider.characterCausingDamage = characterWieldingWeapon;
            meleeDamageCollider.physicalDamage = weapon.physicalDamage;
            meleeDamageCollider.magicDamage = weapon.magicDamage;
            meleeDamageCollider.fireDamage = weapon.fireDamage;
            meleeDamageCollider.holyDamage = weapon.holyDamage;
        }
    }
}
