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
        public static CameraSystem Instance { get; private set; }
        public Cinemachine.CinemachineVirtualCamera orthoCamera; // VirtualCamera GameObject
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
    }
}

