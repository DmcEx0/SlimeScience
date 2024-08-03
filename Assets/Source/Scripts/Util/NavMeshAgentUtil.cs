using UnityEngine.AI;

namespace SlimeScience.Util
{
    public static class NavMeshAgentUtil
    {
        public static int GetAgentTypeID(AgentTypeIds agentTypeId)
        {
            string agentTypeName = AgentTypeIdDictionary.GetAgentTypeId(agentTypeId);
            int count = NavMesh.GetSettingsCount();
            string[] agentTypeNames = new string[count + 2];
            for (var i = 0; i < count; i++)
            {
                int id = NavMesh.GetSettingsByIndex(i).agentTypeID;
                string name = NavMesh.GetSettingsNameFromID(id);
                if (name == agentTypeName)
                {
                    return id;
                }
            }
            return -1;
        }
    }
}