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

        //ĳ������ �տ� �ν��Ͻ��� �ҷ����� ���ӿ�����Ʈ��.
        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        protected override void Awake()
        {
            //CharacterEquipmentManager ���� ������ ����� AWake �� ������.
            base.Awake();

            //ĳ���Ϳ��� ������ ���� �� �ִ� �κ��丮�� ������� ������.
            character = GetComponent<CharacterBase>();

            // Slots ��������.
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
                //weaponSlot �� WeaponModelInstantiationSlot�� ����� ���� WeaponModelSlot weaponSlot�� �ش�.
                //WeaponModelSlot�� Enums�� ����Ǿ��ִ� ������Ÿ�Կ� �ش�.
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
            //�̷��� buff �� ����� ���⸦ �ٽ� �θ��� ���� ���ʸ� �ٽ� �θ��⸦ ���� �� �ֱ� ������
            //BothHands�� �Ѽ��� �ٸ��� �δ� ������ �� �� ����.
            LoadLeftWeapon();
            LoadRightWeapon();
        }
        
        public void LoadRightWeapon()
        {
            if (character.playerInventoryManager.currentRightHandWeapon != null)
            {
                //�Ʒ��� ���� ������ �߻���ų �ٵ�, �������� �ҷ����� �� �ƴ� ����� �ٷ� ��ȯ�ϱ� ������.
                // �׷��� ������, �������� �ҷ����ִ� ������ ���ľ���.
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
