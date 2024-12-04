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

        // ��ü ȸ��
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

        // ��ü ���� ��ü�� ȸ���ϴ°� ó�� �������� ��� �� ��ü�� ��ü �������� ȸ���ϸ鼭 ���ÿ� ��ü�� �ݴ�� ȸ���ؼ� ��ü�� �����ǰ� ��ü�� ���󰡴°�ó�� ����.
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
