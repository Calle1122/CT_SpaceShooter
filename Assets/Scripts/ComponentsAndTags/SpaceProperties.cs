using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct SpaceProperties : IComponentData
    {
        public float2 SpaceDimensions;
        public int NumberOfSpawners;
        public Entity SpawnerPrefab;
        public Entity EnemyPrefab;
        public float EnemySpawnRate;
    }

    public struct EnemySpawnTimer : IComponentData
    {
        public float Value;
    }
}