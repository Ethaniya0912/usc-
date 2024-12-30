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
            // �ٸ� �÷��̾� ����Ʈ/������̾�� �⺻ ���¹̳� �������� ��
            // �ٸ� ���� ��ũ��Ʈ���� ��ȭ�� �ֱ⺸��, �ش� ��ũ��Ʈ������ ó���ϵ��� ��.

            //if (character.IsOwner)
            //{
                // ��Ƽ�÷��� Ȱ����̿�.
                //character.characterNetworkManager.currentStamina.Value -= staminaDamage;
            //}
            //SP �� StaminaPoint�� ������. �˾ƺ� �� �ְ� ���°� ���ڴ�.
            character.curStat.CharacterData.SP -= staminaDamage;
        }
    }
}
