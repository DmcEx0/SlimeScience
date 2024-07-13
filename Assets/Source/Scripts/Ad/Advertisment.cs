using SlimeScience.PauseSystem;
using UnityEngine;

namespace SlimeScience.Ad
{
    public class Advertisment
    {
        private Coroutine _schedule;
        private PauseHandler _systemPause;
        private MonoBehaviour _coroutineObject;

        private bool _isActive = false;

        public Advertisment(
            MonoBehaviour coroutineObject,
            PauseHandler systemPause)
        {
            _systemPause = systemPause;
            _coroutineObject = coroutineObject;
        }

        public void Show()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.InterstitialAd.Show(
                onOpenCallback: OnOpenedCallback,
                onCloseCallback: OnClosedCallback,
                onErrorCallback: OnErrorCallback,
                onOfflineCallback: OnOfflineCallback);
#else
            _systemPause.Unpause();
#endif
        }

        private void OnOpenedCallback()
        {
            _systemPause.Pause();
        }

        private void OnClosedCallback(bool isClosed)
        {
            _systemPause.Unpause();
        }

        private void OnErrorCallback(string error)
        {
            Debug.LogError($"Ad error: {error}");
            _systemPause.Unpause();
        }

        private void OnOfflineCallback()
        {
            _systemPause.Unpause();
        }
    }
}
