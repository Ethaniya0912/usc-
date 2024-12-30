using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterBase characterCausingDamage; // �������� �ٸ� ĳ���ͷκ��� �߻��Ѵٸ� ���⿡ ������.
                                                     // ĳ���Ͱ� ������ ������̾ ���� �� �ֱ� ������ �ʿ�� ��.

        [Header("Damage")]
        public float physicalDamage = 0; // �̷��� �⺻, Ÿ��, ����, ��� ������ ������.
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        public float finalDamage = 0; // ��� ����� �̷�� �� �� ĳ���Ͱ� �޴� ������.
        public override void ProcessEffect(CharacterBase character)
        {
            base.ProcessEffect(character);
        }
    }
}
