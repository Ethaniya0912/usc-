using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        CharacterBase character;
        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        //캐릭터의 손에 인스턴스로 불러와줄 게임오브젝트용.
        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        protected override void Awake()
        {
            //CharacterEquipmentManager 에서 선언한 버츄얼 AWake 을 가져옴.
            base.Awake();

            //캐릭터에서 유저가 가질 수 있는 인벤토리를 끌어오기 위함임.
            character = GetComponent<CharacterBase>();

            // Slots 가져오기.
            InitializeWeaponSlots();
        }

        protected override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                //weaponSlot 은 WeaponModelInstantiationSlot에 선언된 변수 WeaponModelSlot weaponSlot에 해당.
                //WeaponModelSlot은 Enums에 선언되어있는 열거형타입에 해당.
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
                {
                    leftHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            //미래에 buff 등 양손의 무기를 다시 부르기 보단 한쪽만 다시 부르기를 원할 수 있기 때문에
            //BothHands와 한손을 다르게 두는 식으로 할 수 있음.
            LoadLeftWeapon();
            LoadRightWeapon();
        }
        
        public void LoadRightWeapon()
        {
            if (character.playerInventoryManager.currentRightHandWeapon != null)
            {
                //아래는 대충 에러를 발생시킬 텐데, 프리펩을 불러오는 게 아닌 어셋을 바로 소환하기 때문임.
                // 그렇기 때문에, 프리펩을 불러와주는 과정을 거쳐야함.
                //rightHandSlot.LoadWeapon(character.playerInventoryManager.currentRightHandWeapon.weaponModel);

                rightHandWeaponModel = Instantiate(character.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);
                //rightHandWeaponModel.gameObject.SetActive(true);
            }
        }

        public void LoadLeftWeapon()
        {
            if (character.playerInventoryManager.currentLeftHandWeapon != null)
            {
                leftHandWeaponModel = Instantiate(character.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);
                //leftHandWeaponModel.gameObject.SetActive(true);
            }
        }
    }
}
