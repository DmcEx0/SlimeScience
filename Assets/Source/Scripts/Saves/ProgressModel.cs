namespace SlimeScience.Saves
{
    public class ProgressModel
    {
        private const int DefaultMoney = 0;
        private const int DefaultSlimes = 0;
        private const int DefaultRoomIndex = 0;
        private const int DefaultCollectedSlimes = 0;
        private const int DefaultSlimesGoal = 1;
        private const bool DefaultTutorialPassed = false;
        
        public ProgressModel()
        {
            Money = DefaultMoney;
            Slimes = DefaultSlimes;
            RoomIndex = DefaultRoomIndex;
            CollectedSlimes = DefaultCollectedSlimes;
            SlimesGoal = DefaultSlimesGoal;
            TutorialPassed = DefaultTutorialPassed;
        }

        public ProgressModel(
            int money,
            int slimes,
            int roomIndex,
            int collectedSlimes,
            int slimesGoal,
            bool tutorialPassed)
        {
            Money = money;
            Slimes = slimes;
            RoomIndex = roomIndex;
            CollectedSlimes = collectedSlimes;
            SlimesGoal = slimesGoal;
            TutorialPassed = tutorialPassed;
        }

        public int Money { get; private set; }

        public int Slimes { get; private set; }

        public int RoomIndex { get; private set; }

        public int CollectedSlimes { get; private set; }

        public int SlimesGoal { get; private set; }

        public bool TutorialPassed { get; private set; }

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

        public void SetTutorialPassed()
        {
            TutorialPassed = true;
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
