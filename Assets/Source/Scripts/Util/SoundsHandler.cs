using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Util
{
    public static class SoundsHandler
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
    }
}
