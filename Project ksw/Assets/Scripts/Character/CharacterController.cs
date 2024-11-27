using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterBase character;

        public Transform cameraPivot;

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            CameraSystem.Instance.SetCameraFollowTarget(character.cameraPivot);
        }

        private void Update()
        {
            character.Move(InputSystem.Instance.Movement, Camera.main.transform.eulerAngles.y);

        }
    }
}
