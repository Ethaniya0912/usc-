using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USC
{
    [System.Serializable]
    public class GameDataDTO { }

    [System.Serializable]
    public class CharacterDataDTO : GameDataDTO
    {
        public float HP;
        public float SP;

        public float WalkSpeed = 1f;
        public float RunSpeed = 2.5f;

        public float RunStaminaCost = 3f;
        public float StaminaRecoverySpeed = 2f;
    }
}
