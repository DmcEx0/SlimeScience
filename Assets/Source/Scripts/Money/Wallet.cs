using System;
using SlimeScience.Saves;

namespace SlimeScience.Money
{
    public class Wallet
    {
        private GameVariables _gameVariables;

        public Wallet(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
        }

        public event Action MoneyChanged;

        public int Money => _gameVariables.Money;

        public void Spend(int amount)
        {
            _gameVariables.SpendMoney(amount);
            MoneyChanged?.Invoke();
        }

        public void Add(int amount)
        {
            _gameVariables.AddMoney(amount);
            MoneyChanged?.Invoke();
        }

        public bool IsEnoughMoney(int money)
        {
            if (money < 0)
            {
                return false;
            }

            return _gameVariables.Money >= money;
        }
    }
}
