using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class DamageMultiflier : MonoBehaviour
    {
        [field: SerializeField] public float DamageMultiplier { get; set; } = 1.0f;
        [field: SerializeField] public HumanBodyBones HumanBodyBones { get; set; }

    }
}
