using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("Debug Delete Later")]
        //아래 instantCharacterEffect 는 원본을 안쓰고 인스턴스를 생성해서 해당 값을 바꾸기 위함임.
        [SerializeField] InstantCharacterEffect effectToTest;

        //아래 TakeStaminaEffect 는 base StaminaDamage를 직접적으로 바꾸기 위해 테스트하는 것임.
        //[SerializeField] TakeStaminaEffect effectToTest;
        [SerializeField] bool processEffect = false;

        private void Update()
        {
            if (processEffect)
            {
                processEffect = false;
                // 왜 이 카피를 인스턴스 하는가? >> Scriptable Object 에 설정되어있는 기본데미지를 바꾸지 않기위함
                // 기본데미지가 바뀌게 되면 여러 방면에서 문제가 발생할 수 있음.
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProcessInstantEffect(effect);

                // 가령, 아래와 같은 식은 직접적으로 base 값을 바꾸게 됨.
                //effectToTest.staminaDamage = 55;
                //ProcessInstantEffect(effectToTest);
            }
        }
    }
}
