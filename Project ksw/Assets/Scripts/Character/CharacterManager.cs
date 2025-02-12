using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
