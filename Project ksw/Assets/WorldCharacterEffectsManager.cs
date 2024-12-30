using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        // �پ��� ����Ʈ�� �������� ����Ʈȭ ����.
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        private void Awake()
        {
            //get,set ���� �ص� ��.
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
