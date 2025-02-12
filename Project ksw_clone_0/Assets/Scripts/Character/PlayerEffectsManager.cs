using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("Debug Delete Later")]
        //�Ʒ� instantCharacterEffect �� ������ �Ⱦ��� �ν��Ͻ��� �����ؼ� �ش� ���� �ٲٱ� ������.
        [SerializeField] InstantCharacterEffect effectToTest;

        //�Ʒ� TakeStaminaEffect �� base StaminaDamage�� ���������� �ٲٱ� ���� �׽�Ʈ�ϴ� ����.
        //[SerializeField] TakeStaminaEffect effectToTest;
        [SerializeField] bool processEffect = false;

        private void Update()
        {
            if (processEffect)
            {
                processEffect = false;
                // �� �� ī�Ǹ� �ν��Ͻ� �ϴ°�? >> Scriptable Object �� �����Ǿ��ִ� �⺻�������� �ٲ��� �ʱ�����
                // �⺻�������� �ٲ�� �Ǹ� ���� ��鿡�� ������ �߻��� �� ����.
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProcessInstantEffect(effect);

                // ����, �Ʒ��� ���� ���� ���������� base ���� �ٲٰ� ��.
                //effectToTest.staminaDamage = 55;
                //ProcessInstantEffect(effectToTest);
            }
        }
    }
}
