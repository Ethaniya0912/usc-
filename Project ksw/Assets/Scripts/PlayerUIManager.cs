using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace KSW
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance { get; private set; }
        [Header("Network JOIN")]
        [SerializeField] bool startGameAsClient;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false; // 게임이 시작되자마자 바로 제한.
                // 타이틀 스크린에서 호스트로 시작하기 때문에 셧다운 해준다.
                NetworkManager.Singleton.Shutdown();
                // 클라이언트로 재시작.
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
