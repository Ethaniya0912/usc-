using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace USC
{
    // BootStrapper : 에디터상에서 Main을 타고오지않고, 실행했을 경우에 대한 시스템 초기화 도우미 클래스
    public class BootStrapper : MonoBehaviour
    {
        public static bool IsSystemLoaded { get; private set; } = false;

        private static readonly List<string> AutoBootStrapperScenes = new List<string>()
        {
            "Ingame",
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SystemBoot()
        {
            IsSystemLoaded = false;

#if UNITY_EDITOR
            var activeScene = EditorSceneManager.GetActiveScene();
            for (int i = 0; i < AutoBootStrapperScenes.Count; i++)
            {
                if (activeScene.name.Equals(AutoBootStrapperScenes[i]))
                {
                    InternalBoot();
                    break;
                }
            }
#else
            InternalBoot();
#endif

            IsSystemLoaded = true;
        }

        private static void InternalBoot()
        {
            UIManager.Singleton.Initialize();
            SoundManager.Singleton.Initialize();
            GameDataModel.Singleton.Initialize();
            UserDataModel.Singleton.Initialize();

            //TODO : Add more system initialize

            var activeScene = SceneManager.GetActiveScene();
            bool isValidSceneName = false;
            foreach (var scene in AutoBootStrapperScenes)
            {
                if (activeScene.name.Equals(scene))
                {
                    isValidSceneName = true;
                    break;
                }
            }

            if (isValidSceneName)
            {
                //UIManager.Show<IngameUI>(UIList.IngameUI);
                //UIManager.Show<InteractionUI>(UIList.InteractionUI);
                //UIManager.Show<MinimapUI>(UIList.MinimapUI);
                //UIManager.Show<CrosshairUI>(UIList.CrosshairUI);
                //UIManager.Show<IndicatorUI>(UIList.IndicatorUI);

                SoundManager.Singleton.PlayMusic(MusicFileName.BGM_02);
            }
        }
    }
}
