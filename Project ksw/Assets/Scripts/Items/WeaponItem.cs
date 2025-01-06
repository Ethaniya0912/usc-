using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class WeaponItem : Items
    {
        // Animator COntroller Override (���� Ȱ�� ���� ���⿡ ���� ���� �ִϸ��̼� ��ȭ)

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
        // ����Ʈ ���� ���¹̳� �Ҹ� ������̾�
        // ��� ���� ���¹̳� �ڽ�Ʈ ������̾�
    }
}
