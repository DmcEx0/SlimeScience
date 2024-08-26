using System;
using DG.Tweening;
using SlimeScience.Money;
using SlimeScience.Saves;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Upgrades
{
    public abstract class UpgradeViewport : MonoBehaviour
    {
        [SerializeField] private RectTransform _viewport;

        private Tweener _hideTweener;
        private Tweener _showTweener;

        public Wallet Wallet { get; private set; }

        public GameVariables Variables { get; private set; }

        public virtual void Init(Wallet wallet, GameVariables gameVariables)
        {
            Wallet = wallet;
            Variables = gameVariables;
        }

        public abstract void UpdateUI();

        public void Hide()
        {
            KillTweeners();

            _hideTweener = _viewport.DOAnchorPosY(-_viewport.rect.height, 0.5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void Show()
        {
            KillTweeners();

            gameObject.SetActive(true);
            _showTweener = _viewport.DOAnchorPosY(0, 0.5f);
        }

        protected void UpgradeClick(Action<float> upgradeCallback, UpgradeButton upgradeButton, int cost)
        {
            if (Wallet.IsEnoughMoney(cost))
            {
                Wallet.Spend(cost);
                upgradeButton.Upgrade();
                upgradeCallback?.Invoke(upgradeButton.Value);
                Variables.Save();
            }

            UpdateUI();
            SoundsManager.PlayTapUI();
        }

        protected void MakeUpgradeAccessible(UpgradeButton upgradeButton, int cost)
        {
            bool isEnoughMoney = Wallet.IsEnoughMoney(cost);

            if (isEnoughMoney)
            {
                upgradeButton.SetInteractable();
            }
            else
            {
                upgradeButton.SetNotInteractable();
            }
        }

        private void KillTweeners()
        {
            if (_hideTweener != null)
            {
                _hideTweener.Kill();
            }

            if (_showTweener != null)
            {
                _showTweener.Kill();
            }
        }
    }
}
