namespace SlimeScience.Saves
{
    public class ShipModel
    {
        const float DefaultCapacity = 10;
        const float DefaultSpeed = 10;

        public ShipModel()
        {
            Capacity = DefaultCapacity;
            Speed = DefaultSpeed;
        }

        public ShipModel(float capacity, float speed)
        {
            Capacity = capacity;
            Speed = speed;
        }

        public float Capacity { get; private set; }

        public float Speed { get; private set; }

        public void SetCapacity(float capacity)
        {
            if (capacity < 0 || capacity < Capacity)
            {
                return;
            }

            Capacity = capacity;
        }

        public void SetSpeed(float speed)
        {
            if (speed < 0 || speed < Speed)
            {
                return;
            }

            Speed = speed;
        }
    }
}
