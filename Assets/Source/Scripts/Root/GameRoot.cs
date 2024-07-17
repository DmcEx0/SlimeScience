using System.Collections;
using Agava.YandexGames;
using SlimeScience.Saves;
using SlimeScience.Util;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SlimeScience.Root
{
    public class GameRoot : MonoBehaviour
    {
        private const string TutorialCompleted = "Tutorial";
        private const string TutorialSceneName = "Tutorial";
        private const string GameSceneName = "Game";
        private const string _percentTemplate = "{0}%";
        private const float _loadingSpeed = 1.0f;
        private const float _progressThreshold = 0.85f;

        [SerializeField] private Slider _loader;
        [SerializeField] private TMP_Text _percentText;

        private GameVariables _saveObject;
        private bool _dataReceived = false;
        private bool _dataProcessed = false;

        private string _sceneName = "Game";

        private IEnumerator Start()
        {
            _sceneName = PlayerPrefs.GetInt(TutorialCompleted, 0) == 1
                ? GameSceneName
                : TutorialSceneName;

#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();

            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(
                LocalizationUtil.Languages[YandexGamesSdk.Environment.i18n.lang]);
#endif

            yield return StartCoroutine(LoadSceneWithProgressBar());
        }

        private IEnumerator LoadSceneWithProgressBar()
        {
            float progressMax = 1f;
            int percentMultipplier = 100;
            int progressMin = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
            asyncOperation.allowSceneActivation = false;

            _loader.value = progressMin;

            while (!asyncOperation.isDone)
            {
                float progress = asyncOperation.progress < _progressThreshold
                    ? asyncOperation.progress : progressMax;

                _loader.value = Mathf.Lerp(
                    _loader.value,
                    progress,
                    _loadingSpeed * Time.deltaTime);

                string percent = string.Format(
                    _percentTemplate,
                    (int)(_loader.value * percentMultipplier));

                _percentText.text = percent;

                if (_loader.value >= _progressThreshold)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
