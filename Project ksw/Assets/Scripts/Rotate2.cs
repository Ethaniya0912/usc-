using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class Rotation2 : MonoBehaviour
    {
        public Transform upper;
        public Transform lower;
        // public Transform lower;
        public float rotationSpeed;
        public float followDelay;
        public Quaternion angle;
        Quaternion currentRotation;

        // Start is called before the first frame update
        void Start()
        {
            rotationSpeed = 100;
            followDelay = 10;
        }

        // Update is called once per frame
        void Update()
        {
            RotationBody();
            FollowingBody();
        }

        // 상체 회전
        void RotationBody()
        {
            if (Input.GetKey("q"))
            {
                upper.Rotate(new Vector3((rotationSpeed * Time.deltaTime), 0, 0));
                currentRotation = upper.rotation;
            }
            else if (Input.GetKey("e"))
            {
                upper.Rotate(new Vector3(-(Time.deltaTime * rotationSpeed), 0, 0));
                currentRotation = upper.rotation;
                
            }
        }

        // 상체 따라서 하체가 회전하는것 처럼 보이지만 사실 몸 전체가 상체 방향으로 회전하면서 동시에 상체가 반대로 회전해서 상체는 고정되고 하체가 따라가는것처럼 보임.
        void FollowingBody()
        {
            if (Vector3.Equals(upper.eulerAngles, /*lower*/transform.eulerAngles) == false)
            {
                angle = Quaternion.Euler(currentRotation.eulerAngles.y, currentRotation.eulerAngles.x, currentRotation.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * rotationSpeed / followDelay);
                //upper.rotation = currentRotation;
            }
        }
    }
}
