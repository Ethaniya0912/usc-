using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    [System.Serializable]
    public class GameDataDTO { }

    [System.Serializable]
    public class CharacterDataDTO : GameDataDTO
    {
        public float Blood;
        public float Head;
        public float Chest;
        public float LeftArm;
        public float RightArm;
        public float LeftLeg;
        public float RightLeg;
        public float SP;

        public float WalkSpeed = 1f;
        public float RunSpeed = 2.5f;

        public float RunStaminaCost = 3f;
        public float StaminaRecoverySpeed = 2f;
    }
}
