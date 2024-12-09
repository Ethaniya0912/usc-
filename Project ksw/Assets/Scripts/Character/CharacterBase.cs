using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace KSW
{
    public class CharacterBase : MonoBehaviour
    {
        public Animator characterAnimator;
        public UnityEngine.CharacterController unityCharacterController;
        public Rig aimRig;
        //public RigBuilder rigBuilder;
        public Transform cameraPivot;
        public Transform upper;

        public float speed;
        public float armed;
        public float horizontal;
        public float vertical;
        public float runningBlend;
        public bool attackTriggerBool;

        public float moveSpeed = 3f;
        public float targetRotation = 0f;
        public float rotationSpeed = 0.1f;
        public float followDelay = 0.01f;
        Quaternion currentRotation;

        // bool ���µ�
        public bool IsArmed { get; set; } = false;

        public Vector3 AimingPoint
        {
            get => aimingPointTransform.position;
            set => aimingPointTransform.position = value;
        }

        // IK �� Socket �� ��.
        public Transform aimingPointTransform;

        //CheckGround ��
        private float verticalVelocity = 0f;
        private bool isGrounded = false;
        public float groundOffset = 0.1f;
        public float checkRadius = 0.1f;
        public LayerMask groundLayers;

        //Freefall ��
        private float fallingspeed = 0.05f;

        public bool IsRun { get; set; } = false;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            // armed �������� �ƴ��� Ȯ�� �� armed �� �����ֱ�.
            // Lerp (A ��, B��, �ɸ��� �ð�) <-A���� B������ �������ֱ�.
            // �Ʒ����� IsArmed �Ͻ� True �� 1��, �ƴϸ� 0���� ���ߴ� ��.
            armed = Mathf.Lerp(armed, IsArmed ? 1f : 0f, Time.deltaTime * 10);
            runningBlend = Mathf.Lerp(runningBlend, IsRun ? 1f : 0f, Time.deltaTime * 10f);

            //CheckGround();
            //FreeFall();


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
            if (IsArmed)
            {
                movement = transform.forward * vertical + transform.right * horizontal;
                moveSpeed = 1.5f;
            }

            else
            {
                if (input.magnitude > 0f)
                {
                    targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + yAxisAngle;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, 0.1f);
                    transform.rotation = Quaternion.Euler(0f, rotation, 0f);
                    //transform.rotation = Quaternion.Euler(0f, rotation, 0f);
                }

                movement = transform.forward * speed;
                moveSpeed = 3f;
            }

            movement.y = verticalVelocity;

            unityCharacterController.Move(movement * Time.deltaTime * moveSpeed);
        }

        public void Rotate(float angle)
        {
            float rotation = transform.rotation.eulerAngles.y + angle;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        public void CheckGround()
        {
            Ray ray = new Ray(transform.position + (Vector3.up * groundOffset), Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.distance);
                if (hit.distance > 5) isGrounded = false;
                //else isGrounded = true;
            }
            //isGrounded = Physics.SphereCast(ray, checkRadius, 0.1f, groundLayers);
        }

        public void FreeFall()
        {
            if(!isGrounded)
            {
                verticalVelocity = Mathf.Lerp(verticalVelocity, -9.8f, (Time.deltaTime * fallingspeed));
                Debug.Log(verticalVelocity);
            }
            else
            {
                verticalVelocity = 0f;
            }
        }

        public void SetArmed(bool isArmed)
        {
            IsArmed = isArmed;
            if (IsArmed)
            {
                characterAnimator.SetTrigger("Equip Trigger");
            }
            else
            {
                characterAnimator.SetTrigger("Holster Trigger");
            }
        }

        public void Attack()
        {
            if (IsArmed)
            {
                characterAnimator.SetBool("AttackTriggerBool", true);
            }

        }

        public void Idle()
        {
            if (IsArmed)
            {
                characterAnimator.SetBool("AttackTriggerBool", false);
            }
        }
    }
}
