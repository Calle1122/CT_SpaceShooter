using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct EnemySpawnPoints : IComponentData
    {
        public BlobAssetReference<EnemySpawnPointsBlob> Value;
    }

    public struct EnemySpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}