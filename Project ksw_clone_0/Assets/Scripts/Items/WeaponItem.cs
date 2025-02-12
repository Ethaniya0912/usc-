using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class WeaponItem : Items
    {
        // Animator COntroller Override (현재 활용 중인 무기에 따라 공격 애니메이션 변화)

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Requirements")]
        public int strengthREQ = 0;
        //public int dexREQ = 0;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int magicDamage = 0;
        public int fireDamage = 0;
        public int holyDamage = 0;

        [Header("Weapon Poise Damage")]
        public int poiseDamage = 0;
        // Offensive poise bonus when attacking

        // Weapon Modifiers
        // Light attack Modifiers
        // Heavy attack modifier
        // critical damage modifier etc

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        // 라이트 어택 스태미나 소모 모디파이어
        // 헤비 어택 스태미나 코스트 모디파이어
    }
}
