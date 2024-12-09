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

        // bool 상태들
        public bool IsArmed { get; set; } = false;

        public Vector3 AimingPoint
        {
            get => aimingPointTransform.position;
            set => aimingPointTransform.position = value;
        }

        // IK 및 Socket 등 용.
        public Transform aimingPointTransform;

        //CheckGround 용
        private float verticalVelocity = 0f;
        private bool isGrounded = false;
        public float groundOffset = 0.1f;
        public float checkRadius = 0.1f;
        public LayerMask groundLayers;

        //Freefall 용
        private float fallingspeed = 0.05f;

        public bool IsRun { get; set; } = false;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            characterAnimator.SetLayerWeight(2, 0);
        }

        private void Update()
        {
            // armed 상태인지 아닌지 확인 후 armed 로 맞춰주기.
            // Lerp (A 값, B값, 걸리는 시간) <-A값을 B값으로 움직여주기.
            // 아래에선 IsArmed 일시 True 면 1로, 아니면 0으로 맞추는 것.
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

        public void RotateToTargetPoint(Vector3 targetPoint)
        {
            //direction은 targetPoint(마우스커서가 갈 지점)에서 트랜스폼의 포지션을 빼줄 것
            // 둘 사이의 각도를 제거해주기 위한 벡터를 하나 잡아줌.
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f; // 이걸 안해주면 허리가 승천함.

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            //transform.rotation 값은 Quaternion 값으로 처리되어있기때문에, Euler 값인 Vector3값등을 바로 던져주면 안됨.
            //즉, 위에 있는 Quaternion 값은 밑에 transform.rotation값에 대입해주기 위해서 Quaternioin으로 변환해준 다음에 대입한 것임.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }

        //public void Rotate(float angle)
        //{
        //    //private float delayedRotationSpeed = rotationSpeed * Time.deltaTime / followDelay;
        //    float rotation = Mathf.Lerp(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y + angle, Time.deltaTime * 10);
        //    transform.rotation = Quaternion.Euler(0, rotation, 0);
        //}

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
            if (!isGrounded)
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
            characterAnimator.SetLayerWeight(2, 1);
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
                characterAnimator.SetBool("StanceBool", true);
            }

        }

        public void Idle()
        {
            if (IsArmed)
            {
                characterAnimator.SetBool("StanceBool", false);
            }
        }
    }
}
