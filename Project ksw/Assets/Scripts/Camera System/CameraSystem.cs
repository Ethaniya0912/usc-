using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        // Body IK를 위한 AimingPointTransform 을 카메라에서 자동으로 잡아주기 위한 코드.
        public Vector3 AimingPoint { get; private set; }
        public LayerMask aimingLayerMask;

        private CameraType currentCameraType = CameraType.Ortho;
        private bool isZoom = false;

        public Transform neck;

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
            // Aiming Point 계산
            // ScreenPointToRay 는 마우스가 도달하는 곳으로 정확히 따라감. Ray를 쏘아 맞닿은 부분에
            // 도달하는 식이여서, 해당 지점의 벡터까지 잘 잡음. 굳.
            Vector3 ScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Ray ray = Camera.main.ScreenPointToRay(ScreenPoint);
            //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayerMask, QueryTriggerInteraction.Ignore))
            {
                AimingPoint = hitInfo.point;
            }
            else
            {
                AimingPoint = Vector3.zero;
            }
        }
    }
}

