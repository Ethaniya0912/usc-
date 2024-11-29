using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterBase character;

        public Transform cameraPivot;

        public float bottomClamp;
        public float topClamp;
        private float targetYaw;
        private float targetPitch;

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

            character.IsRun = InputSystem.Instance.IsLeftShift;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            if (InputSystem.Instance.Look.sqrMagnitude > 0f)
            {
                float yaw = InputSystem.Instance.Look.x;
                float pitch = InputSystem.Instance.Look.y;

                targetYaw += yaw;
                targetPitch -= pitch;
            }

            targetYaw = ClampAngle(targetYaw, float.MinValue, float.MaxValue);
            targetPitch = ClampAngle(targetPitch, bottomClamp, topClamp);
            cameraPivot.rotation = Quaternion.Euler(targetPitch, targetYaw, 0f);
        }

        private float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
