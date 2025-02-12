using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0; // 미래엔 기본, 타격, 베기, 찌르기 등으로 나뉠것.
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;

        [Header("Contact Point")]
        protected Vector3 contactPoint;

        [Header("Character Damaged")]
        protected List<CharacterBase> characterDamaged = new List<CharacterBase>();

        private void OnTriggerEnter(Collider other)
        {
            CharacterBase damageTarget = other.GetComponent<CharacterBase>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Check if we can damage this target based on friendly fire (캐릭터가 아군공격을 하는지 체크)

                // 타겟이 블럭 중인지 확인

                // 타겟이 공격할 수 없는 대상인지 확인

                // 데미지
                DamageTarget(damageTarget);
            }
        }   

        protected virtual void DamageTarget(CharacterBase damageTarget)
        {
            // 한번의 공격이 동일 타겟에 여러번의 데미지를 발생시키기 원하지 않음.
            // 각 사지가 존재할 때, 여러번의 데미지를 발생시키려할 수 있기 떄문에 이를 처리할것.

            if (characterDamaged.Contains(damageTarget))
                return;

            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.contactPoint = contactPoint;

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            characterDamaged.Clear(); // 콜라이더를 리셋 시 접촉한 캐릭터 또한 리셋, 다시 맞도록 준비.
        }
    }
}
