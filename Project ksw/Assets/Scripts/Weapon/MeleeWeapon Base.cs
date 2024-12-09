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
        private CharacterBase owner; // 무기를 소유한 캐릭터 객체.
    }
}
