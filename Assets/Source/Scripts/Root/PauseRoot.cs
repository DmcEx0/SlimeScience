using SlimeScience.PauseSystem;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Root
{
    public class PauseRoot : MonoBehaviour
    {
        private const float PauseTimeScale = 0;
        private const float UnpauseTimeScale = 1;

        private PauseHandler[] _pauseSources;

        private void OnEnable()
        {
            if (_pauseSources != null)
            {
                foreach (var pause in _pauseSources)
                {
                    pause.Changed += OnPauseChanged;
                }
            }
        }

        private void OnDisable()
        {
            if (_pauseSources != null)
            {
                foreach (var pause in _pauseSources)
                {
                    pause.Changed -= OnPauseChanged;
                }
            }
        }

        public void Init(PauseHandler[] pauses)
        {
            _pauseSources = pauses;

            foreach (var pause in _pauseSources)
            {
                pause.Changed += OnPauseChanged;
            }
        }

        private void Pause()
        {
            foreach (var pause in _pauseSources)
            {
                if (pause.IsPaused)
                {
                    Time.timeScale = PauseTimeScale;
                    SoundsManager.PauseBG();
                    return;
                }
            }
        }

        private void Unpause()
        {
            foreach (var pause in _pauseSources)
            {
                if (pause.IsPaused)
                {
                    return;
                }
            }

            Time.timeScale = UnpauseTimeScale;
            SoundsManager.UnpauseBG();
        }

        private void OnPauseChanged(bool isPaused)
        {
            if (isPaused)
            {
                Pause();
                return;
            }

            Unpause();
        }
    }
}
