using ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace AuthoringAndMono
{
    public class SpaceMono : MonoBehaviour
    {
        public float2 SpaceDimensions;
        public int NumberOfSpawners;
        public GameObject SpawnerPrefab;
        public uint RandomSeed;
        public GameObject EnemyPrefab;
        public float EnemySpawnRate;
    }
    
    public class SpaceBaker : Baker<SpaceMono>
    {
        public override void Bake(SpaceMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpaceProperties
            {
                SpaceDimensions = authoring.SpaceDimensions,
                NumberOfSpawners = authoring.NumberOfSpawners,
                SpawnerPrefab = GetEntity(authoring.SpawnerPrefab, TransformUsageFlags.Dynamic),
                EnemyPrefab =  GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                EnemySpawnRate = authoring.EnemySpawnRate
            });
            AddComponent(entity, new SpaceRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<EnemySpawnPoints>(entity);
            AddComponent<EnemySpawnTimer>(entity);
        }
    }
}