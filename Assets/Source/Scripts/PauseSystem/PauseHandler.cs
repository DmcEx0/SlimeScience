using System;

namespace SlimeScience.PauseSystem
{
    public class PauseHandler
    {
        private bool _isPaused;

        public event Action<bool> Changed;

        public bool IsPaused => _isPaused;

        public void Pause()
        {
            _isPaused = true;
            Changed?.Invoke(true);
        }

        public void Unpause()
        {
            _isPaused = false;
            Changed?.Invoke(false);
        }
    }
}
