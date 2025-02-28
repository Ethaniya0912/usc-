using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace KSW
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // Process Instant Effects (Take Damage, Heal) 즉각 효과 처리
        // Process Timed Effects (Poison, Build UPS) 시간차를 두고 처리
        // Process Static Effects (장비착용에 따른 버프 추가/제거)

        CharacterBase character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterBase>();
        }
        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            // Take in an Effect
            // Process it
            effect.ProcessEffect(character);
        }
    }
}
