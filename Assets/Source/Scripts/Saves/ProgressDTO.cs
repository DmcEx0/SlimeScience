using System;

namespace SlimeScience.Saves
{
    [Serializable]
    public class ProgressDTO
    {
        public ProgressDTO(
            int money,
            int slimes,
            int roomIndex,
            int collectedSlimes,
            int slimesGoal,
            bool tutorialPassed,
            float absorptionForce,
            float absorptionRadius,
            float absorptionAngle,
            float absorptionCapacity,
            float assistantCount,
            float shipSpeed,
            float shipCapacity)
        {
            Money = money;
            Slimes = slimes;
            RoomIndex = roomIndex;
            CollectedSlimes = collectedSlimes;
            SlimesGoal = slimesGoal;
            TutorialPassed = tutorialPassed;
            AbsorptionForce = absorptionForce;
            AbsorptionRadius = absorptionRadius;
            AbsorptionAngle = absorptionAngle;
            AbsorptionCapacity = absorptionCapacity;
            AssistantCount = assistantCount;
            ShipSpeed = shipSpeed;
            ShipCapacity = shipCapacity;
        }

        public int Money;

        public int Slimes;

        public int RoomIndex;

        public int CollectedSlimes;

        public int SlimesGoal;

        public bool TutorialPassed;

        public float AbsorptionForce;

        public float AbsorptionRadius;

        public float AbsorptionAngle;

        public float AbsorptionCapacity;

        public float AssistantCount;

        public float ShipSpeed;

        public float ShipCapacity;
    }
}
