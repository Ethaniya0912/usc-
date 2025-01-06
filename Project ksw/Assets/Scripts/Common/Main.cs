using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace KSW
{
    // ���ǻ��� : SceneType Enum ���� ����, Scene ������ �̸��� �����ؾ� �Ѵ�.
    public enum SceneType
    {
        None,
        Empty,
        Title,
        Ingame,
    }

    // Main : ������ �������� ��, ���� ���� ����Ǵ� Entry ������ �ϴ� Ŭ�����̴�.
    public class Main : SingletonBase<Main>
    {
        // #1. ���� ������ Scene�� �����ϴ� SceneController ������ ����
        // #2. ������ �ʱ�ȭ ������ ����ϴ� ����

        public SceneType CurrentSceneType => currentSceneType;

        [SerializeField] private SceneType currentSceneType = SceneType.None;

        private bool isInitialize = false;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (isInitialize)
                return;

            isInitialize = true;

            // TODO : �� �� �ʿ��� �ý����� �ʱ�ȭ
            UIManager.Singleton.Initialize();
            SoundManager.Singleton.Initialize();
            GameDataModel.Singleton.Initialize();
            UserDataModel.Singleton.Initialize();

            // ù ����Ǵ� ������ ��ȯ => Title Scene
#if UNITY_EDITOR
            Scene activeScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            if (activeScene.name == "Main")
            {
                ChangeScene(SceneType.Title);
            }
#else
            ChangeScene(SceneType.Title);
#endif
        }

        public void SystemQuit()
        {
            // ���� ���� ���
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


        public SceneBase CurrentSceneController => currentSceneController;
        public bool IsOnProgressSceneChanging { get; private set; } = false;

        private SceneBase currentSceneController = null;

        public void ChangeScene(SceneType sceneType, System.Action sceneLoadedCallback = null)
        {
            if (currentSceneType == sceneType)
                return;

            switch (sceneType)
            {
                case SceneType.Title:
                    //ChangeScene<TitleScene>(sceneType, sceneLoadedCallback);
                    break;
                case SceneType.Ingame:
                    //ChangeScene<IngameScene>(sceneType, sceneLoadedCallback);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        private void ChangeScene<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
        {
            if (IsOnProgressSceneChanging)
                return;

            StartCoroutine(ChangeSceneAsync<T>(sceneType, sceneLoadedCallback));
        }

        private IEnumerator ChangeSceneAsync<T>(SceneType sceneType, System.Action sceneLoadedCallback = null)
            where T : SceneBase
        {
            IsOnProgressSceneChanging = true;

            // Show Loading UI
            //var loadingUI = UIManager.Show<LoadingUI>(UIList.LoadingUI);
            //loadingUI.SetProgress(0f);

            if (currentSceneController != null)
            {
                yield return currentSceneController.OnEnd();
                Destroy(currentSceneController.gameObject);
            }

            //loadingUI.SetProgress(0.1f);
            yield return null;

            AsyncOperation emtpyOperation = SceneManager.LoadSceneAsync(SceneType.Empty.ToString(), LoadSceneMode.Single);
            yield return new WaitUntil(() => emtpyOperation.isDone);

            //loadingUI.SetProgress(0.3f);
            yield return null;

            GameObject sceneGo = new GameObject(typeof(T).Name);
            sceneGo.transform.parent = transform;
            currentSceneController = sceneGo.AddComponent<T>();
            currentSceneType = sceneType;

            yield return StartCoroutine(currentSceneController.OnStart());

            //loadingUI.SetProgress(1f);
            yield return null;

            // Hide Loading UI
            //UIManager.Hide<LoadingUI>(UIList.LoadingUI);

            IsOnProgressSceneChanging = false;
            sceneLoadedCallback?.Invoke();
        }
    }
}
