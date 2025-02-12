using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class MeleeDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterBase characterCausingDamage;
    }
}
