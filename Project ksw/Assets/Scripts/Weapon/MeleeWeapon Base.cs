using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using USC;

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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(
                "<color=yellow> Sword Impact !! </color>" +
                $"name : <color=red>{other.gameObject.name}</color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("HitScanner"))
            {
                /*Quaternion rotation = Quaternion.LookRotation(other.ClosestPoint);*/
                EffectType targetEffectType = EffectType.HitBlood;
                Vector3 position = other.ClosestPoint( other.transform.position );
                Quaternion rotation = Quaternion.LookRotation(other.ClosestPoint(other.transform.position ));
                EffectManager.Instance.CreateEffect(targetEffectType, position, rotation);

                if (other.transform.root.TryGetComponent(out IDamage damageInterface))
                {
                    float damageMultiple = 1f;
                    if (other.gameObject.TryGetComponent(out DamageMultiflier multiplier))
                    {
                        damageMultiple = multiplier.DamageMultiplier;
                    }
                    damageInterface.ApplyDamage(10 * damageMultiple);
                }
            }
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    //TODO : �浹 �� � ������Ʈ�� �ε��ƴ°�?
        //    // => ����/�Ǵ�
        //    // => �Ϲ� ������Ʈ�̸� ����Ʈ�� ��� || ĳ���� ������Ʈ�� ������ + ���� ����Ʈ.
        //
        //    //Debug.Log(
        //    //    "<color=yellow> Sword Impact !! </color>" +
        //    //    $"name : <color=red>{collision.gameObject.name}</color>");
        //
        //    if (collision.gameObject.layer == LayerMask.NameToLayer("HitScanner"))
        //    {
        //        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        //
        //        if (collision.transform.root.TryGetComponent(out IDamage damageInterface))
        //        {
        //            float damageMultiple = 1f;
        //            if (collision.gameObject.TryGetComponent(out DamageMultiflier multiplier))
        //            {
        //                damageMultiple = multiplier.DamageMultiplier;
        //            }
        //            damageInterface.ApplyDamage(10 * damageMultiple);
        //        }
        //    }
        //}
    }
}
