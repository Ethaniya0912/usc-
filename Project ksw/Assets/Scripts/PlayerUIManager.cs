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
                startGameAsClient = false; // ������ ���۵��ڸ��� �ٷ� ����.
                // Ÿ��Ʋ ��ũ������ ȣ��Ʈ�� �����ϱ� ������ �˴ٿ� ���ش�.
                NetworkManager.Singleton.Shutdown();
                // Ŭ���̾�Ʈ�� �����.
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
