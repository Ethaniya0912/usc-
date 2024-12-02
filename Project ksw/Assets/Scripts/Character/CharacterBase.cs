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
        public Transform upper;

        public float speed;
        public float armed;
        public float horizontal;
        public float vertical;
        public float runningBlend;

        public float moveSpeed = 3f;
        public float targetRotation = 0f;
        public float rotationSpeed = 0.1f;
        public float followDelay = 0.01f;
        Quaternion currentRotation;

        //CheckGround ¿ë
        private float verticalVelocity = 0f;
        private bool isGrounded = false;
        public float groundOffset = 0.1f;
        public float checkRadius = 0.1f;
        public LayerMask groundLayers;

        //Freefall ¿ë
        private double fallingspeed = 0.1;

        public bool IsRun { get; set; } = false;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            runningBlend = Mathf.Lerp(runningBlend, IsRun ? 1f : 0f, Time.deltaTime * 10f);

            CheckGround();
            FreeFall();

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
                //transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            movement = transform.forward * speed;
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
            }
            else
            {
                verticalVelocity = 0f;
            }
        }
    }
}
