using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KSW
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager Instance { get; private set; }

        [SerializeField] int worldSceneIndex = 1;

        private void Awake()
        {
            // �ش� �ν��Ͻ��� �ϳ��� �����ؾ��ϸ�, �׷��� ���� �� �ı�.
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

        // �̳ʹ·�����(�ڷ�ƾish), �񵿱������� ���� �θ� ��.
        // �̴� ���� �ε����� ����ϸ鼭 Ʈ��ŷ�� �� �ֵ��� �ϴ� ������� Ȱ��

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperator = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }
    }
}
