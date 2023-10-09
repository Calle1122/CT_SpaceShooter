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
    }
    
    public class SpaceBaker : Baker<SpaceMono>
    {
        public override void Bake(SpaceMono authoring)
        {
            AddComponent(new SpaceProperties
            {
                SpaceDimensions = authoring.SpaceDimensions,
                NumberOfSpawners = authoring.NumberOfSpawners,
                SpawnerPrefab = GetEntity(authoring.SpawnerPrefab)
            });
            AddComponent(new SpaceRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}