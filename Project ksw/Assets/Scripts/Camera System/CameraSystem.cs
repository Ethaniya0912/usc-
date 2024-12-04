using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public enum CameraType
    {
        Ortho,
    }
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; } =null;
        public Cinemachine.CinemachineVirtualCamera orthoCamera; // VirtualCamera GameObject

        // Body IK�� ���� AimingPointTransform �� ī�޶󿡼� �ڵ����� ����ֱ� ���� �ڵ�.
        public Vector3 AimingPoint { get; private set; }
        public LayerMask aimingLayerMask;

        private CameraType currentCameraType = CameraType.Ortho;
        private bool isZoom = false;

        public void SetCameraFollowTarget(Transform target)
        {
            orthoCamera.Follow = target;
            orthoCamera.LookAt = target;
        }

        private void Awake()
        {
            Instance = this;

            orthoCamera.gameObject.SetActive(true);
        }

        private void Update()
        {
            // Aiming Point ���
            // ScreenPointToRay �� ���콺�� �����ϴ� ������ ��Ȯ�� ����. Ray�� ��� �´��� �κп�
            // �����ϴ� ���̿���, �ش� ������ ���ͱ��� �� ����. ��.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayerMask, QueryTriggerInteraction.Ignore))
            {
                AimingPoint = hitInfo.point;
            }
            else
            {
                AimingPoint = ray.GetPoint(100f);
            }
        }
    }
}

