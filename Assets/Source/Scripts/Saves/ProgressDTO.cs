using System;

namespace SlimeScience.Saves
{
    [Serializable]
    public class ProgressDTO
    {
        public ProgressDTO(
            int money,
            int slimes,
            float absorptionForce,
            float absorptionRadius,
            float absorptionAngle,
            float absorptionCapacity,
            float assistantCount)
        {
            Money = money;
            Slimes = slimes;
            AbsorptionForce = absorptionForce;
            AbsorptionRadius = absorptionRadius;
            AbsorptionAngle = absorptionAngle;
            AbsorptionCapacity = absorptionCapacity;
            AssistantCount = assistantCount;
        }

        public int Money;

        public int Slimes;

        public float AbsorptionForce;

        public float AbsorptionRadius;

        public float AbsorptionAngle;

        public float AbsorptionCapacity;

        public float AssistantCount;
    }
}
