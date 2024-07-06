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
         
         public static void PlayUnloadSlime()
         {
             s_audioSource.PlayOneShot(s_config.UnloadSlime);
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
    }
}
