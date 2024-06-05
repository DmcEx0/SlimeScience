namespace SlimeScience.Saves
{
    public class ProgressModel
    {
        private const int DefaultMoney = 0;
        private const int DefaultSlimes = 0;
        
        public ProgressModel()
        {
            Money = DefaultMoney;
            Slimes = DefaultSlimes;
        }

        public ProgressModel(int money, int slimes)
        {
            Money = money;
            Slimes = slimes;
        }

        public int Money { get; private set; }

        public int Slimes { get; private set; }

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

        public void AddSlimes(int slimes)
        {
            if (slimes < 0)
            {
                return;
            }

            Slimes += slimes;
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
