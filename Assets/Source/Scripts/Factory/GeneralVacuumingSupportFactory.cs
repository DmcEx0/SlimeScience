
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralVacuumingSupport", menuName = "Factories/General/VacuumingSupport")]
    public class GeneralVacuumingSupportFactory : VacuumingSupportFactory
    {
        [SerializeField] private VacuumingSupportConfig _config;
        
        protected override VacuumingSupportConfig GetConfig()
        {
            return _config;
        }
    }
}
