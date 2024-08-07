using SlimeScience.Audio;
using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Util
{
    public static class SoundsManager
    {
        private const string Background = "Background";
        private const string Sfx = "SFX";
        private const int TurnOnBoolean = 1;

        private static SoundsConfig s_config;
        private static AudioSource s_backgroundAudioSource;
        private static AudioSource s_sfxAudioSource;

        private static bool _backgroundIsOn = true;
        private static bool _sfxIsOn = true;

        public static void Initialize(SoundsConfig config, AudioSource bgAudioSource, AudioSource sfxAudioSource)
        {
            s_config = config;
            s_backgroundAudioSource = bgAudioSource;
            s_sfxAudioSource = sfxAudioSource;

            _backgroundIsOn = PlayerPrefs.GetInt(Background) == TurnOnBoolean;
            _sfxIsOn = PlayerPrefs.GetInt(Sfx) == TurnOnBoolean;
        }
        
        public static void TurnOn(SoundType type)
        {
            switch (type)
            {
                case SoundType.Background:
                    SwitchBg(true);
                    break;
                case SoundType.Sfx:
                    SwitchSfx(true);
                    break;
            }
        }

        public static void TurnOff(SoundType type)
        {
            switch (type)
            {
                case SoundType.Background:
                    SwitchBg(false);
                    break;
                case SoundType.Sfx:
                    SwitchSfx(false);
                    break;
            }
            
        }

        private static void SwitchBg(bool isOn)
        {
            _backgroundIsOn = isOn;
            
            if(_backgroundIsOn == false)
            {
                PauseBG();
                return;
            }
            
            UnpauseBG();

            if (s_backgroundAudioSource.isPlaying == false)
            {
                PlayBgMusic();
            }
        }

        private static void SwitchSfx(bool isOn)
        {
            _sfxIsOn = isOn;
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
            if (_backgroundIsOn == false) 
            {
                return;
            }

            s_backgroundAudioSource.clip = s_config.BackgroundMusic;
            s_backgroundAudioSource.loop = true;
            s_backgroundAudioSource.Play();
        }

        public static void PauseBG()
        {
            s_backgroundAudioSource.Pause();
        }

        public static void UnpauseBG()
        {
            if (_backgroundIsOn == false)
            {
                return;
            }

            s_backgroundAudioSource.UnPause();
        }

        private static void PlaySound(AudioClip clip)
        {
            if (_sfxIsOn && clip != null)
            {
                s_sfxAudioSource.PlayOneShot(clip);
            }
        }
    }
}
