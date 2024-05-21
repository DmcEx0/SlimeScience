using Agava.YandexGames;
using Lean.Localization;
using TMPro;
using UnityEngine;

namespace SlimeScience.Leaderbords
{
    public class LBPlayerContainer : MonoBehaviour
    {
        private const string AnonymousTranslation = "anonymous";

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void Render(LeaderboardEntryResponse entry)
        {
            string anonymous = LeanLocalization.GetTranslationText(AnonymousTranslation);

            _name.text = string.IsNullOrEmpty(entry.player.publicName)
                ? anonymous : entry.player.publicName;

            _score.text = entry.score.ToString();
        }
    }
}
