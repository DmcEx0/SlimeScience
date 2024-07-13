using UnityEngine;

namespace SlimeScience.Configs
{
    [CreateAssetMenu(fileName = nameof(SoundsConfig), menuName = "Util/Sounds/" + nameof(SoundsConfig), order = 0)]
    public class SoundsConfig : ScriptableObject
    {
        [field:SerializeField] public AudioClip TapUIClickSound { get; private set; }
        [field:SerializeField] public AudioClip SlimesCatchSound { get; private set; }
        [field:SerializeField] public AudioClip BackgroundMusic { get; private set; }
        [field:SerializeField] public AudioClip UnloadSlime { get; private set; }
        [field:SerializeField] public AudioClip Trap { get; private set; }
        [field:SerializeField] public AudioClip LevelOpened { get; private set; }
    }
}