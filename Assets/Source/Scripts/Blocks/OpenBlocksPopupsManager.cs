using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Blocks
{
    public class OpenBlocksPopupsManager : MonoBehaviour
    {
        [SerializeField] private List<OpenBlocksPopup> _popups;

        public void ShowPopup(int index)
        {
            _popups[index - 1].Show();
        }
    }
}
