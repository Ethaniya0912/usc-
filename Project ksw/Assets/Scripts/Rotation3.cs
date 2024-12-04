using UnityEngine;

namespace KSW
{
    public class Rotation3 : MonoBehaviour
    {
        public Transform upper;         // ��ü ������Ʈ
        public Transform upperGoal;    // ��ü ȸ�� ���� ������Ʈ - ��ü�� transform���� ���ƾ���, �θ� �޶� ��ӵǾ� �����ϴ� ���� ��� ����.
        public Transform bodyGoal;     // ��ü ȸ�� ���� ������Ʈ - ��ü ȸ�� ������ ����
        public float rotationSpeed;     // ƽ�� ȸ�� �ӵ�(����)
        public float followDelay;       // ������� ȸ��ü �ӵ� ����.
        Quaternion goalBodyRotationAngle;  // ��ü ���� ȸ����
        Quaternion goalUpperRotationAngle; // ��ü ���� ȸ����


        // Start is called before the first frame update
        void Start()
        {
            rotationSpeed = 500.0f;
            followDelay = 50.0f;
        }

        // Update is called once per frame
        void Update()
        {
            RotationUpperBody();
            FollowingUpperBody();
        }

        // ��ü ȸ��
        void RotationUpperBody()
        {
            // left turn
            if (Input.GetKey("q"))
            {
                upper.Rotate(new Vector3(-rotationSpeed * Time.deltaTime, 0.0f, 0.0f));
            }
            // right turn
            else if (Input.GetKey("e"))
            {
                upper.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0.0f, 0.0f));
            }

            if (goalBodyRotationAngle != bodyGoal.rotation)
            {
                // ��ü�� ȸ���Ϸ��� ����� �ش�Ǵ� ȸ����
                goalBodyRotationAngle = bodyGoal.rotation;
            }

            if (goalUpperRotationAngle != upperGoal.rotation)
            {
                // ��ü�� ȸ���Ϸ��� ����� �ش�Ǵ� ȸ����
                goalUpperRotationAngle = upperGoal.rotation;
            }
        }

        /*
         * ��ü ���� ��ü�� ȸ���ϴ°� ó�� ��������
         * ��� �� ��ü�� ��ü �������� ȸ���ϸ鼭 
         * ���ÿ� ��ü�� ���� �ݴ�� ȸ���ؼ� 
         * ��ü�� �����ǰ� ��ü�� ���󰡴°�ó�� ����
         */
        void FollowingUpperBody()
        {
            // ��ü�� ���� ȸ������ ���� ���� ȸ������ ������ ������ ����
            if (Vector3.Equals(goalBodyRotationAngle.eulerAngles, transform.eulerAngles) == false)
            {
                float delayedRotationSpeed = rotationSpeed * Time.deltaTime / followDelay;

                transform.rotation = Quaternion.Lerp(transform.rotation, goalBodyRotationAngle, delayedRotationSpeed);

                upper.rotation = Quaternion.Lerp(upper.rotation, goalUpperRotationAngle, delayedRotationSpeed);
            }
        }
    }
}