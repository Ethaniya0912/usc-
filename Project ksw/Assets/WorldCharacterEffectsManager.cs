using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        // 다양한 이펙트가 있음으로 리스트화 해줌.
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        private void Awake()
        {
            //get,set 으로 해도 됨.
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void GenerateEffectsIDs()
        {
            for (int i = 0; i < instantEffects.Count; ++i)
            {
                instantEffects[i].instantEffectID = i;
            }
        }
    }
}
