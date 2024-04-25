namespace SlimeScience.Saves
{
    public class AbsorptionModel
    {
        private const float DefaultForce = 10.0f;
        private const float DefaultRadius = 5.0f;
        private const float DefaultAngle = 55.0f;
        private const int DefaultCapacity = 10;

        public AbsorptionModel()
        {
            Force = DefaultForce;
            Radius = DefaultRadius;
            Angle = DefaultAngle;
            Capacity = DefaultCapacity;
        }

        public AbsorptionModel(float force, float radius, float angle, int capacity)
        {
            Force = force;
            Radius = radius;
            Angle = angle;
            Capacity = capacity;
        }

        public float Force { get; private set; }

        public float Radius { get; private set; }

        public float Angle { get; private set; }

        public int Capacity { get; private set; }
    }
}