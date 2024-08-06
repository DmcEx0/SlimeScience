using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Blocks
{
    public class OpenBlocksPopupsManager : MonoBehaviour
    {
        [SerializeField] private List<OpenBlocksPopup> _popups;
        [SerializeField] private OpenBlocksPopup _finishGamePopup;

        public void ShowPopup(int index)
        {
            _popups[index - 1].Show();
        }
        
        public void ShowEndGamePopup(Action onClosed)
        {
            _finishGamePopup.Show(onClosed);
        }
    }
}
