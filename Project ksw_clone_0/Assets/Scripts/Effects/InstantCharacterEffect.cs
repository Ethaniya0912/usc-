using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class InstantCharacterEffect : ScriptableObject
    {
        [Header("Effect ID")]
        //���߿� ���۷����� �����ϵ��� public ���� ����.
        public int instantEffectID;

        public virtual void ProcessEffect(CharacterBase character)
        {

        }
    }
}
