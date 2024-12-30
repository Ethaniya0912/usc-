using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterBase characterCausingDamage; // 데미지가 다른 캐릭터로부터 발생한다면 여기에 저장함.
                                                     // 캐릭터가 데미지 모디파이어가 있을 수 있기 때문에 필요로 함.

        [Header("Damage")]
        public float physicalDamage = 0; // 미래엔 기본, 타격, 베기, 찌르기 등으로 나뉠것.
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        public float finalDamage = 0; // 모든 계산이 이루어 진 후 캐릭터가 받는 데미지.
        public override void ProcessEffect(CharacterBase character)
        {
            base.ProcessEffect(character);
        }
    }
}
