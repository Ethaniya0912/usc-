using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class MeleeWeaponBase : MonoBehaviour
    {
        public CharacterBase Owner
        {
            get => owner;
            set => owner = value;
        }

        private Cinemachine.CinemachineImpulseSource impulseSource;
        private CharacterBase owner; // ���⸦ ������ ĳ���� ��ü.
    }
}
