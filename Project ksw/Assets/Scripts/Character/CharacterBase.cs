using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class CharacterBase : MonoBehaviour
    {
        public Animator characterAnimator;
        public UnityEngine.CharacterController unityCharacterController;
        //public RigBuilder rigBuilder;
        public Transform cameraPivot;

        public float speed;
        public float armed;
        public float horizontal;
        public float vertical;
        public float runningBlend;

        public float moveSpeed = 3f;
        public float targetRotation = 0f;
        public float rotationSpeed = 0.1f;

        public bool IsRun { get; set; } = false;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            runningBlend = Mathf.Lerp(runningBlend, IsRun ? 1f : 0f, Time.deltaTime * 10f);

            characterAnimator.SetFloat("Speed", speed);
            characterAnimator.SetFloat("Armed", armed);
            characterAnimator.SetFloat("Horizontal", horizontal);
            characterAnimator.SetFloat("Vertical", vertical);
            characterAnimator.SetFloat("RunningBlend", runningBlend);
        }

        public void Move(Vector2 input, float yAxisAngle)
        {
            horizontal = input.x;
            vertical = input.y;
            speed = input.magnitude > 0f ? 1f : 0f;

            Vector3 movement = Vector3.zero;

            if(input.magnitude > 0f)
            {
                targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + yAxisAngle;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, 0.1f);
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            movement = transform.forward * speed;

            unityCharacterController.Move(movement * Time.deltaTime * moveSpeed);
        }

        public void Rotate(float angle)
        {
            float rotation = transform.rotation.eulerAngles.y + angle;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}