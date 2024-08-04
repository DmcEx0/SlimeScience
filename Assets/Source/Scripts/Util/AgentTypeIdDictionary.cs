using System.Collections.Generic;

namespace SlimeScience.Util
{
    internal class AgentTypeIdDictionary
    {
        private static IReadOnlyDictionary<AgentTypeIds, string> AgentTypeNamesDictionary = new Dictionary<AgentTypeIds, string>
        {
            {AgentTypeIds.Player, "Humanoid"},
            {AgentTypeIds.Ship, "Ship"}
        };

        public static string GetAgentTypeId(AgentTypeIds agentType)
        {
            return AgentTypeNamesDictionary[agentType];
        }
    }
}
