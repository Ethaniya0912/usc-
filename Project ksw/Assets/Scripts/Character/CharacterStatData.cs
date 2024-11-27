using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USC
{
    [CreateAssetMenu(fileName = "Character Stat Data", menuName = "USC/Character/Character Stat Data")]
    public class CharacterStatData : ScriptableObject
    {
        public CharacterDataDTO CharacterData = new CharacterDataDTO();
    }
}

