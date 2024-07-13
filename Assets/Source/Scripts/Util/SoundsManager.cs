using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Util
{
    public static class SoundsManager
    {
         private static SoundsConfig s_config;
         private static AudioSource s_audioSource;
        
         public static void Initialize(SoundsConfig config, AudioSource audioSource)
         {
             s_config = config;
             s_audioSource = audioSource;
         }

         public static void PlayTapUI()
         {
             s_audioSource.PlayOneShot(s_config.TapUIClickSound);
         }
         
         public static void PlaySlimeCatch()
         {
             s_audioSource.PlayOneShot(s_config.SlimesCatchSound);
         }

        public static void PlayExplode()
        {
            s_audioSource.PlayOneShot(s_config.Explode);
        }
         
         public static void PlayUnloadSlime()
         {
             s_audioSource.PlayOneShot(s_config.UnloadSlime);
         }
         public static void PlayTrap()
         {
             s_audioSource.PlayOneShot(s_config.Trap);
         }      
         public static void PlayLevelOpened()
         {
             s_audioSource.PlayOneShot(s_config.LevelOpened);
         }
         
         public static void PlayBgMusic()
         {
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
            s_audioSource.UnPause();
        }
    }
}
