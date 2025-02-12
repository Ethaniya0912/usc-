using UnityEngine;

namespace KSW
{
    public class Aiming1 : MonoBehaviour
    {
        [SerializeField]
        Transform headTarget;
        [SerializeField]
        Transform spineTarget;
        [SerializeField]
        Transform bodyTarget;

        Vector3 worldHitPos;

        bool isMove;

        private void Start()
        {
            isMove = false;
            worldHitPos = new Vector3();
        }

        // Update is called once per frame
        void Update()
        {
            CheckMove();
            GetMousePos();
            MoveAimingTarget();
        }

        private void LateUpdate()
        {
            RotationToTarget();
        }

        // ���콺 ��ġ�� ���� �� rayhit ����
        void GetMousePos()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit))
            {
                worldHitPos = hit.point;
            }
        }

        // �̵��ϴ��� üũ
        void CheckMove()
        {
            isMove = (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) ? true : false;
        }

        // ��ü ���콺 ���� ȸ��
        void MoveAimingTarget()
        {
            float x = worldHitPos.x;
            float z = worldHitPos.z;
            headTarget.position = new Vector3(x, headTarget.position.y, z);
            spineTarget.position = new Vector3(x, spineTarget.position.y, z);

            Vector3 bodyToTarget = new Vector3(worldHitPos.x, bodyTarget.position.y, worldHitPos.z) - bodyTarget.position;
            bodyTarget.rotation = Quaternion.LookRotation(bodyToTarget);
        }

        // ��ü ȸ��
        void RotationToTarget()
        {
            if (!isMove)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, bodyTarget.rotation, Time.deltaTime);
            }
        }
    }
}
