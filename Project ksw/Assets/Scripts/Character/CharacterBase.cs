using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace KSW
{
    public class CharacterBase : MonoBehaviour, IDamage
    {
        [Header("Managers")]
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;

        [Header("Status")]
        public bool isDead = false;
        //public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermisson.Owner);
        //위는 네트워크로 작성시.

        public Animator characterAnimator;
        public UnityEngine.CharacterController unityCharacterController;
        public Rig aimRig;
        //public RigBuilder rigBuilder;
        public Transform cameraPivot;
        public Transform upper;

        //래그돌용 콜라이더
        public Collider[] ragdollColliders;
        public Rigidbody[] ragdollRigidbodies;

        public float speed;
        public float armed;
        public float horizontal;
        public float vertical;
        public float runningBlend;
        public bool attackTrigger;

        public float moveSpeed = 3f;
        public float targetRotation = 0f;
        public float rotationSpeed = 0.1f;
        public float followDelay = 0.01f;
        Quaternion currentRotation;

        //체력등
        public CharacterStatData maxStat;
        public CharacterStatData curStat;

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
        private bool isGrounded = true;
        public float groundOffset = 0.1f;
        public float checkRadius = 0.1f;
        public LayerMask groundLayers;

        //Freefall 용
        private float fallingspeed = 0.4f;

        public bool IsRun { get; set; } = false;

        //Damage 적용용
        public System.Action<float, float> OnDamaged;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            characterAnimator.SetLayerWeight(2, 0);

            Transform hipTransform = characterAnimator.GetBoneTransform(HumanBodyBones.Hips);

            //래그돌 콜라이더, 리지드바디 컴포넌트 불러오기.
            ragdollColliders = hipTransform.GetComponentsInChildren<Collider>();
            ragdollRigidbodies = hipTransform.GetComponentsInChildren<Rigidbody>();

            SetActiveRagdool(false);
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
        }

        private void Update()
        {
            // armed 상태인지 아닌지 확인 후 armed 로 맞춰주기.
            // Lerp (A 값, B값, 걸리는 시간) <-A값을 B값으로 움직여주기.
            // 아래에선 IsArmed 일시 True 면 1로, 아니면 0으로 맞추는 것.
            armed = Mathf.Lerp(armed, IsArmed ? 1f : 0f, Time.deltaTime * 10);
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

            //unityCharacterController.Move(movement * Time.deltaTime * moveSpeed);
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
                //Debug.Log(hit.distance);
                if (hit.distance > 0.3)
                {
                    isGrounded = false;
                }
                else isGrounded = true;
            }
            //isGrounded = Physics.SphereCast(ray, checkRadius, 0.1f, groundLayers);
        }

        public void FreeFall()
        {
            if (!isGrounded)
            {
                verticalVelocity = Mathf.Lerp(verticalVelocity, -9.8f, (Time.deltaTime * fallingspeed));
                //Debug.Log(verticalVelocity);
                if (verticalVelocity < -2) SetActiveRagdool(true);
            }
            else
            {
                verticalVelocity = 0f;
                //SetActiveRagdool(false);
                //characterAnimator.SetTrigger("GetBackUp");
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

        public void AttackStance()
        {
            if (IsArmed)
            {
                characterAnimator.SetBool("StanceBool", true);
            }

        }

        public void AttackPoke()
        {
            characterAnimator.SetTrigger("AttackPokeTrigger");
            //characterAnimator.SetLayerWeight(1, 1);

            //aimingRig.Weight = 0;
            //pokeRig.Weight = 1;
        }

        public void AttackSlash()
        {
            characterAnimator.SetTrigger("AttackSlashTrigger");
        }

        //public void OnAnimatorIK(int layerIndex)
        //{
        //    if (layerIndex == 2)
        //    {
        //        var stateInfo = characterAnimator.GetCurrentAnimatorStateInfo(2);
        //        if (stateInfo.IsName("Poke"))
        //        {
        //            characterAnimator.SetIKPosition(AvatarIKGoal.RightHand, );
        //        }
        //    }
        //}

        public void Idle()
        {
            if (IsArmed)
            {
                characterAnimator.SetBool("StanceBool", false);
            }
        }

        public void SetActiveRagdool(bool isActive)
        {
            characterAnimator.enabled = !isActive;
            //unityCharacterController.enabled = !isActive;
            for (int i = 0; i < ragdollRigidbodies.Length; i++)
            {
                ragdollRigidbodies[i].isKinematic = !isActive;
            }
        }

        //CharacterLimbLists limb = MeleeWeaponBase.HitLimbType;

        public void ApplyDamage(float damage, CharacterLimbLists hitLimbType)
        {
            //curStat.CharacterData.Blood -= damage;
            
            OnDamaged?.Invoke(maxStat.CharacterData.Blood, curStat.CharacterData.Blood);
            //Debug.Log(curStat.CharacterData.Blood);
            //MeleeWeaponBase hitobj;

            //CharacterLimbLists limb = MeleeWeaponBase.Owner;
            //var limb = hitobj.GetComponent<MeleeWeaponBase>.HitLimbType; 
            //
            if (hitLimbType == CharacterLimbLists.Head)
             {
                 curStat.CharacterData.Head -= damage;
                Debug.Log( "Head" + curStat.CharacterData.Head);
             }
            else if (hitLimbType == CharacterLimbLists.Chest)
             {
                 curStat.CharacterData.Chest -= damage;
                Debug.Log( "Chest" + curStat.CharacterData.Chest);
            }
            else if (hitLimbType == CharacterLimbLists.LeftArm)
             {
                 curStat.CharacterData.LeftArm -= damage;
                Debug.Log("LeftArm"+curStat.CharacterData.LeftArm);
            }
            else if (hitLimbType == CharacterLimbLists.RightArm)
             {
                 curStat.CharacterData.RightArm -= damage;
                Debug.Log("RightArm"+curStat.CharacterData.RightArm);
            }
          
        }
    }
}
