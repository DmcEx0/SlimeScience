namespace SlimeScience.Saves
{
    public class AbsorptionModel
    {
        private const float DefaultForce = 50f;
        private const float DefaultRadius = 5.0f;
        private const float DefaultAngle = 55f;
        private const float DefaultCapacity = 3;
        private const float DefaultAssistantCount = 0;

        public AbsorptionModel()
        {
            Force = DefaultForce;
            Radius = DefaultRadius;
            Angle = DefaultAngle;
            Capacity = DefaultCapacity;
            AssistantCount = DefaultAssistantCount;
        }

        public AbsorptionModel(float force, float radius, float angle, float capacity, float assistantCount)
        {
            Force = force;
            Radius = radius;
            Angle = angle;
            Capacity = capacity;
            AssistantCount = assistantCount;
        }

        public float Force { get; private set; }

        public float Radius { get; private set; }

        public float Angle { get; private set; }

        public float Capacity { get; private set; }

        public float AssistantCount { get; private set; }

        public void SetForce(float force)
        {
            if (force < 0 || force < Force)
            {
                return;
            }

            Force = force;
        }

        public void SetRadius(float radius)
        {
            if (radius < 0 || radius < Radius)
            {
                return;
            }

            Radius = radius;
        }

        public void SetAngle(float angle)
        {
            if (angle < 0 || angle < Angle)
            {
                return;
            }

            Angle = angle;
        }

        public void SetCapacity(float capacity)
        {
            if (capacity < 0 || capacity < Capacity)
            {
                return;
            }

            Capacity = capacity;
        }

        public void SetAssistant(float assistant)
        {
            if (assistant < 0 || assistant < AssistantCount)
            {
                return;
            }

            AssistantCount = assistant;
        }
    }
}