using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    [CreateAssetMenu(menuName = "KSW/Character Effects/Instant Effects/ Take Stamina Damage")]
    public class TakeStaminaEffect : InstantCharacterEffect
    {
        public float staminaDamage;
        public override void ProcessEffect(CharacterBase character)
        {
            // 
            CalculateStaminaDamage(character);
        }

        public void CalculateStaminaDamage(CharacterBase character)
        {
            // 다른 플레이어 이펙트/모디파이어와 기본 스태미나 데미지를 비교
            // 다른 여러 스크립트에서 변화를 주기보단, 해당 스크립트에서만 처리하도록 함.

            //if (character.IsOwner)
            //{
                // 멀티플레이 활용시이용.
                //character.characterNetworkManager.currentStamina.Value -= staminaDamage;
            //}
            //SP 는 StaminaPoint의 약자임. 알아볼 수 있게 쓰는게 좋겠다.
            character.curStat.CharacterData.SP -= staminaDamage;
        }
    }
}
