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
        private CharacterBase owner; // 무기를 소유한 캐릭터 객체.

        public CharacterLimbLists HitLimbType
        {
            get => hitLimbType;
            set => hitLimbType = value;
        }
        private CharacterLimbLists hitLimbType;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(
                "<color=yellow> Sword Impact !! </color>" +
                $"name : <color=red>{other.gameObject.name}</color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("HitScanner"))
            {
                ///*Quaternion rotation = Quaternion.LookRotation(other.ClosestPoint);*/
                //EffectType targetEffectType = EffectType.HitBlood;
                //Vector3 position = other.ClosestPoint( other.transform.position );
                //Quaternion rotation = Quaternion.LookRotation(other.ClosestPoint(other.transform.position ));
                //EffectManager.Instance.CreateEffect(targetEffectType, position, rotation);

                if (other.transform.root.TryGetComponent(out IDamage damageInterface))
                {
                    float damageMultiple = 1f;
                    if (other.gameObject.TryGetComponent(out DamageMultiflier multiplier))
                    {
                        damageMultiple = multiplier.DamageMultiplier;

                        switch (multiplier.HumanBodyBones)
                        {
                            case HumanBodyBones.Head:
                                hitLimbType = CharacterLimbLists.Head;
                                break;
                            case HumanBodyBones.Spine:
                            case HumanBodyBones.UpperChest:
                            case HumanBodyBones.Chest:
                                hitLimbType = CharacterLimbLists.Chest;
                                break;
                        }
                    }

                    if (other.gameObject.name.Contains("Head"))
                    {
                        hitLimbType = CharacterLimbLists.Head;
                    }
                    else if (other.gameObject.name.Contains("Chest"))
                    {
                        hitLimbType = CharacterLimbLists.Chest;
                    }
                    else if (other.gameObject.name.Contains("Shoulder_L") || other.gameObject.name.Contains("Elbow_L"))
                    {
                        hitLimbType = CharacterLimbLists.LeftArm;
                    }
                    else if (other.gameObject.name.Contains("Shoulder_R") || other.gameObject.name.Contains("Elbow_R"))
                    {
                        hitLimbType = CharacterLimbLists.RightArm;
                    }
                    else if (other.gameObject.name.Contains("UpperLeg_L"))
                    {
                        hitLimbType = CharacterLimbLists.LeftLeg;
                    }
                    else if (other.gameObject.name.Contains("UpperLeg_R"))
                    {
                        hitLimbType = CharacterLimbLists.RightLeg;
                    }

                    damageInterface.ApplyDamage(10 * damageMultiple, hitLimbType);
                }
                Debug.Log(hitLimbType.ToString());
            }
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    //TODO : 충돌 시 어떤 오브젝트에 부딪쳤는가?
        //    // => 구분/판단
        //    // => 일반 오브젝트이면 이펙트만 출력 || 캐릭터 오브젝트면 데미지 + 출혈 이펙트.
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
