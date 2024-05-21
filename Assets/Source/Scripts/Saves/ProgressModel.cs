namespace SlimeScience.Saves
{
    public class ProgressModel
    {
        private const int DefaultMoney = 0;
        
        public ProgressModel()
        {
            Money = DefaultMoney;
        }

        public ProgressModel(int money)
        {
            Money = money;
        }

        public int Money { get; private set; }

        public void AddMoney(int amount)
        {
            if (amount < 0)
            {
                return;
            }

            Money += amount;
        }

        public void SpendMoney(int amount)
        {
            if (IsEnoughMoney(amount) == false)
            {
                return;
            }

            Money -= amount;
        }

        private bool IsEnoughMoney(int money)
        {
            if (money < 0)
            {
                return false;
            }

            return Money >= money;
        }
    }
}
