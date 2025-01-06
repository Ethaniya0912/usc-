#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KSW
{
    // BootStrapper : 에디터상에서 Main을 타고오지않고, 실행했을 경우에 대한 시스템 초기화 도우미 클래스
    public class BootStrapper : MonoBehaviour
    {
        private const string BootStrapperMenuPath = "KSW/BootStrapper/Activate BootStrapper";

        private static bool IsActivateBootStrapper
        {
            get => UnityEditor.EditorPrefs.GetBool(BootStrapperMenuPath, false);
            set => UnityEditor.EditorPrefs.SetBool(BootStrapperMenuPath, value);
        }

        [UnityEditor.MenuItem(BootStrapperMenuPath, false)]
        private static void ActivateBootStrapper()
        {
            IsActivateBootStrapper = !IsActivateBootStrapper;
            UnityEditor.Menu.SetChecked(BootStrapperMenuPath, IsActivateBootStrapper);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SystemBoot()
        {
            Scene activeScene = EditorSceneManager.GetActiveScene();
            if (IsActivateBootStrapper && false == activeScene.name.Equals("Main"))
            {
                InternalBoot();
            }
        }

        private static void InternalBoot()
        {
            Main.Singleton.Initialize();

            UIManager.Singleton.Initialize();
            SoundManager.Singleton.Initialize();
            GameDataModel.Singleton.Initialize();
            UserDataModel.Singleton.Initialize();

            //TODO : Add more system initialize

            //UIManager.Show<IngameUI>(UIList.IngameUI);
            //UIManager.Show<InteractionUI>(UIList.InteractionUI);
            //UIManager.Show<MinimapUI>(UIList.MinimapUI);
            //UIManager.Show<CrosshairUI>(UIList.CrosshairUI);
            //UIManager.Show<IndicatorUI>(UIList.IndicatorUI);

            SoundManager.Singleton.PlayMusic(MusicFileName.BGM_02);
        }
    }
}
#endif