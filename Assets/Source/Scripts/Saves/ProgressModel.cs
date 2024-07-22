namespace SlimeScience.Saves
{
    public class ProgressModel
    {
        private const int DefaultMoney = 10000;
        private const int DefaultSlimes = 0;
        private const int DefaultRoomIndex = 0;
        private const int DefaultCollectedSlimes = 0;
        private const int DefaultSlimesGoal = 1;
        
        public ProgressModel()
        {
            Money = DefaultMoney;
            Slimes = DefaultSlimes;
            RoomIndex = DefaultRoomIndex;
            CollectedSlimes = DefaultCollectedSlimes;
            SlimesGoal = DefaultSlimesGoal;
        }

        public ProgressModel(
            int money,
            int slimes,
            int roomIndex,
            int collectedSlimes,
            int slimesGoal)
        {
            Money = money;
            Slimes = slimes;
            RoomIndex = roomIndex;
            CollectedSlimes = collectedSlimes;
            SlimesGoal = slimesGoal;
        }

        public int Money { get; private set; }

        public int Slimes { get; private set; }

        public int RoomIndex { get; private set; }

        public int CollectedSlimes { get; private set; }

        public int SlimesGoal { get; private set; }

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

        public void IncreaseRoomIndex()
        {
            RoomIndex++;
        }

        public void AddCollectedSlimes(int slimes)
        {
            if (slimes < 0)
            {
                return;
            }

            CollectedSlimes += slimes;
        }

        public void ResetCollectedSlimes()
        {
            CollectedSlimes = DefaultCollectedSlimes;
        }

        public void SetSlimesGoal(int goal)
        {
            if (goal < 0)
            {
                return;
            }

            SlimesGoal = goal;
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
