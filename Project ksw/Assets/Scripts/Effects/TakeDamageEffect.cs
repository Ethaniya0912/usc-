using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    [CreateAssetMenu(menuName = "KSW/Character Effects/Instant Effects/ Take Damage")]
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

        [Header("Poise")]
        //균형 데미지. 감당 가능한 균형데미지 이상을 받을 경우 스턴상태화.
        public float poiseDamage = 0;
        public bool poiseIsBroken = false; 

        // Todo : Build ups
        // build up effect amounts

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSoundFX; // 다른 메인데미지 sfx와 더불어 속성sfx 재생.

        [Header("Directional Damage Taken From")]
        //맞는 방향에 따라 맞는 데미지 애니메이션을 다르게 재생.
        public float angleHitFrom;               // 데미지 애니메이션이 플레이될 걸 정함(앞으로 이동, 옆등등)
        public Vector3 contactPoint;             // 피fx를 발생시킬 지점(방향도)
                                                 
        [Header("Final Damage")]                 
        public float finalDamage = 0;            // 모든 계산이 이루어 진 후 캐릭터가 받는 데미지.
        public override void ProcessEffect(CharacterBase character)
        {
            base.ProcessEffect(character);

            if (character.isDead)
            //if (character.isDead.Value) // 네트워크사용시.
                return;

            // 회피나 구르기 등을 시전하는지 체크. 그럴 시 return.

            // 데미지 계산
            CalculateDamage(character);
            // 어떤 방향에서 데미지가 오는지 확인.
            // 데미지 애니메이션 재생.
            // build up(포이즌, 피)등 체크
            // 데미지 sfx 재생
            // 데미지 vfx 재생 (피등)

        }

        private void CalculateDamage(CharacterBase character)
        {
            // 캐릭터가 owner 가 아닐 경우 그냥 return
            // if (!character.IsOwner)
            // return

            if (characterCausingDamage != null)
            {
                // 데미지 모디파이어가 있는지 확인하고 베이스 데미지 변경(물리/엘레멘탈 데미지 버프)

            }

            // 기본 방어력을 체크하고 데미지 감소

            // 캐릭터의 아머 흡수를 확인, 데미지에서 %만큼 감소

            // 데미지를 모두 합산한 다음 최종 데미지 finalDamage 에 적용.
            finalDamage = Mathf.Round(physicalDamage + magicDamage + fireDamage + holyDamage);

            if (finalDamage <= 0)
            {
                finalDamage = 1;
            }

            //character.characterNetworkManager.currentHealth.Value -= finalDamage;
            character.curStat.CharacterData.Blood -= finalDamage;
            Debug.Log("Blood"+character.curStat.CharacterData.Blood);
        }
    }
}
