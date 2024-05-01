using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Configs
{
    [CreateAssetMenu(fileName = "BlocksConfig", menuName = "Configs/Blocks")]
    public class BlocksConfig : ScriptableObject
    {
        [SerializeField] private List<BlockData> _blocksData;

        public IReadOnlyList<BlockData> BlocksData => _blocksData;
    }
}
