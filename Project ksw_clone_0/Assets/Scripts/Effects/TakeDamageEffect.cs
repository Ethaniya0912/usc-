using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    [CreateAssetMenu(menuName = "KSW/Character Effects/Instant Effects/ Take Damage")]
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

        [Header("Poise")]
        //���� ������. ���� ������ ���������� �̻��� ���� ��� ���ϻ���ȭ.
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
        public AudioClip elementalDamageSoundFX; // �ٸ� ���ε����� sfx�� ���Ҿ� �Ӽ�sfx ���.

        [Header("Directional Damage Taken From")]
        //�´� ���⿡ ���� �´� ������ �ִϸ��̼��� �ٸ��� ���.
        public float angleHitFrom;               // ������ �ִϸ��̼��� �÷��̵� �� ����(������ �̵�, �����)
        public Vector3 contactPoint;             // ��fx�� �߻���ų ����(���⵵)
                                                 
        [Header("Final Damage")]                 
        public float finalDamage = 0;            // ��� ����� �̷�� �� �� ĳ���Ͱ� �޴� ������.
        public override void ProcessEffect(CharacterBase character)
        {
            base.ProcessEffect(character);

            if (character.isDead)
            //if (character.isDead.Value) // ��Ʈ��ũ����.
                return;

            // ȸ�ǳ� ������ ���� �����ϴ��� üũ. �׷� �� return.

            // ������ ���
            CalculateDamage(character);
            // � ���⿡�� �������� ������ Ȯ��.
            // ������ �ִϸ��̼� ���.
            // build up(������, ��)�� üũ
            // ������ sfx ���
            // ������ vfx ��� (�ǵ�)

        }

        private void CalculateDamage(CharacterBase character)
        {
            // ĳ���Ͱ� owner �� �ƴ� ��� �׳� return
            // if (!character.IsOwner)
            // return

            if (characterCausingDamage != null)
            {
                // ������ ������̾ �ִ��� Ȯ���ϰ� ���̽� ������ ����(����/������Ż ������ ����)

            }

            // �⺻ ������ üũ�ϰ� ������ ����

            // ĳ������ �Ƹ� ����� Ȯ��, ���������� %��ŭ ����

            // �������� ��� �ջ��� ���� ���� ������ finalDamage �� ����.
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
