using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Util
{
    public static class SoundsManager
    {
        private const string Audio = "Audio";
        private const int TurnOnBoolean = 1;

        private static SoundsConfig s_config;
        private static AudioSource s_audioSource;

        private static bool _isOn = true;

        public static void Initialize(SoundsConfig config, AudioSource audioSource)
        {
            s_config = config;
            s_audioSource = audioSource;

            _isOn = PlayerPrefs.GetInt(Audio) == TurnOnBoolean;
        }

        public static void TurnOn()
        {
            _isOn = true;
            UnpauseAll();

            if (s_audioSource.isPlaying == false)
            {
                PlayBgMusic();
            }
        }

        public static void TurnOff()
        {
            _isOn = false;
            PauseAll();
        }

        public static void PlayTapUI() => PlaySound(s_config.TapUIClickSound);

        public static void PlaySlimeCatch() => PlaySound(s_config.SlimesCatchSound);

        public static void PlayExplode() => PlaySound(s_config.Explode);

        public static void PlayUnloadSlime() => PlaySound(s_config.UnloadSlime);

        public static void PlayTrap() => PlaySound(s_config.Trap);

        public static void PlayLevelOpened() => PlaySound(s_config.LevelOpened);

        public static void PlayTeleport() => PlaySound(s_config.Teleport);

        public static void PlayBgMusic()
        {
            if (_isOn == false) 
            {
                return;
            }

            s_audioSource.clip = s_config.BackgroundMusic;
            s_audioSource.loop = true;
            s_audioSource.Play();
        }

        public static void PauseBgMusic()
        {
            s_audioSource.Pause();
        }

        public static void PauseAll()
        {
            s_audioSource.Pause();
        }

        public static void UnpauseAll()
        {
            if (_isOn == false)
            {
                return;
            }

            s_audioSource.UnPause();
        }

        private static void PlaySound(AudioClip clip)
        {
            if (_isOn && clip != null)
            {
                s_audioSource.PlayOneShot(clip);
            }
        }
    }
}
