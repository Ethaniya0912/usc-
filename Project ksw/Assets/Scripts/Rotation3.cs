using UnityEngine;

namespace KSW
{
    public class Rotation3 : MonoBehaviour
    {
        public Transform upper;         // 상체 오브젝트
        public Transform upperGoal;    // 상체 회전 목적 오브젝트 - 상체와 transform값이 같아야함, 부모가 달라 상속되어 변동하는 값과 상관 없음.
        public Transform bodyGoal;     // 본체 회전 목적 오브젝트 - 상체 회전 목적과 같음
        public float rotationSpeed;     // 틱당 회전 속도(각도)
        public float followDelay;       // 따라오는 회전체 속도 저항.
        Quaternion goalBodyRotationAngle;  // 본체 최종 회전값
        Quaternion goalUpperRotationAngle; // 상체 최종 회전값


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

        // 상체 회전
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
                // 본체가 회전하려는 결과에 해당되는 회전값
                goalBodyRotationAngle = bodyGoal.rotation;
            }

            if (goalUpperRotationAngle != upperGoal.rotation)
            {
                // 상체가 회전하려는 결과에 해당되는 회전값
                goalUpperRotationAngle = upperGoal.rotation;
            }
        }

        /*
         * 상체 따라서 하체가 회전하는것 처럼 보이지만
         * 사실 몸 전체가 상체 방향으로 회전하면서 
         * 동시에 상체가 몸과 반대로 회전해서 
         * 상체는 고정되고 하체는 따라가는것처럼 보임
         */
        void FollowingUpperBody()
        {
            // 본체가 최종 회전값과 같지 않은 회전값을 가지고 있으면 실행
            if (Vector3.Equals(goalBodyRotationAngle.eulerAngles, transform.eulerAngles) == false)
            {
                float delayedRotationSpeed = rotationSpeed * Time.deltaTime / followDelay;

                transform.rotation = Quaternion.Lerp(transform.rotation, goalBodyRotationAngle, delayedRotationSpeed);

                upper.rotation = Quaternion.Lerp(upper.rotation, goalUpperRotationAngle, delayedRotationSpeed);
            }
        }
    }
}