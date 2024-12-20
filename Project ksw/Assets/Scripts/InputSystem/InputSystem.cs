using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build;
using UnityEngine;

namespace KSW
{
    public class InputSystem : MonoBehaviour
    {
        public static InputSystem Instance { get; private set; }

        public Vector2 Movement => movement;
        public Vector2 Look => look;

        public bool IsLeftShift => isLeftShift;
        //public bool IsR => isR;

        private Vector2 movement;
        private Vector2 look;
        private bool isLeftShift;
        //private bool isR;
        private bool isShowCursor = false;

        public System.Action OnClickSpace;
        public System.Action OnClickLeftMouseButton;
        public System.Action OnClickRightMouseButton;
        public System.Action OnClickUpMouseWheel;
        public System.Action OnClickR;

        [Header("QUED INPUTS")]
        [SerializeField] private bool input_Que_Is_Active = false;
        [SerializeField] float default_Que_Input_Time = 0.35f;
        [SerializeField] float que_Input_Timer = 0;
        [SerializeField] bool que_Input = false;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClickLeftMouseButton?.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnClickRightMouseButton?.Invoke();
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                OnClickUpMouseWheel?.Invoke();
            }
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            movement = new Vector2(inputX, inputY);

            float lookX = Input.GetAxis("Mouse X");
            float lookY = Input.GetAxis("Mouse Y");
            look = isShowCursor ? Vector2.zero : new Vector2(lookX, lookY);

            isLeftShift = Input.GetKey(KeyCode.LeftShift);
            if (Input.GetKey(KeyCode.R))
            {
                OnClickR?.Invoke();
            }
        }

        private void QuedInput(ref bool quedI) // Passing a reference means we pass a specific bool
                                               // and not the value of that bool (true of false)
        {
            // Reset all qued inputs so only one que at a time

            //que_RB_Input = false;
            //que_RT_Input = false;
            //que_LB_Input = false;
            //que_LT_Input = false;
        }

        private void ProcessQuedInput()
        {
            // 여기엔 캐릭터가 사망시 그냥 return 값을 넣어줄 것.
            // if (player.isDead.Value)
            // return;

            //if (que_Input) { RB_Input = true;}
        }

        private void HandleQuedInPuts()
        {
            if (input_Que_Is_Active)
            {
                // WHILE THE TIMER IS ABOVE 0, Keep Attempting to press the Input.
                if (que_Input_Timer > 0)
                {
                    que_Input_Timer -= Time.deltaTime;
                    ProcessQuedInput();
                }
                else
                {
                    // Reset All Qued Inputs.
                    que_Input = false;

                    input_Que_Is_Active = false;
                    que_Input_Timer = 0;
                }
            }
        }
    }
}
