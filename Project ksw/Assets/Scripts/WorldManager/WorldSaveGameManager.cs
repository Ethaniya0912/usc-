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
            // 해당 인스턴스는 하나만 존재해야하며, 그렇지 않을 시 파괴.
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

        // 이너뮤레이터(코루틴ish), 비동기적으로 씬을 부를 것.
        // 이는 차후 로딩씬을 사용하면서 트래킹할 수 있도록 하는 기능으로 활용

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperator = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }
    }
}
