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
        public float physicalDamage = 0; // �̷��� �⺻, Ÿ��, ����, ��� ������ ������.
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

                // Check if we can damage this target based on friendly fire (ĳ���Ͱ� �Ʊ������� �ϴ��� üũ)

                // Ÿ���� �� ������ Ȯ��

                // Ÿ���� ������ �� ���� ������� Ȯ��

                // ������
                DamageTarget(damageTarget);
            }
        }   

        protected virtual void DamageTarget(CharacterBase damageTarget)
        {
            // �ѹ��� ������ ���� Ÿ�ٿ� �������� �������� �߻���Ű�� ������ ����.
            // �� ������ ������ ��, �������� �������� �߻���Ű���� �� �ֱ� ������ �̸� ó���Ұ�.

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
            characterDamaged.Clear(); // �ݶ��̴��� ���� �� ������ ĳ���� ���� ����, �ٽ� �µ��� �غ�.
        }
    }
}
