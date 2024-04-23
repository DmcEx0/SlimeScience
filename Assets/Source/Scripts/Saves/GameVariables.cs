
using System;

namespace SlimeScience.Saves
{
    public class GameVariables
    {
        private AbsorptionModel AbsorptionModel;

        public float AbsorptionForce => AbsorptionModel.Force;

        public float AbsorptionRadius => AbsorptionModel.Radius;

        public float AbsorptionAngle => AbsorptionModel.Angle;

        public int AbsorptionCapacity => AbsorptionModel.Capacity;

        public event Action Loaded;

        public void Load()
        {
            AbsorptionModel = new AbsorptionModel();

            Loaded?.Invoke();
        }
    }
}
