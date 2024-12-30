using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class InstantCharacterEffect : ScriptableObject
    {
        [Header("Effect ID")]
        //나중에 레퍼런스가 가능하도록 public 으로 만듬.
        public int instantEffectID;

        public virtual void ProcessEffect(CharacterBase character)
        {

        }
    }
}
