using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CreateSpawnersSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpaceProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var spaceEntity = SystemAPI.GetSingletonEntity<SpaceProperties>();
            var space = SystemAPI.GetAspect<SpaceAspect>(spaceEntity);

            var builder = new BlobBuilder(Allocator.Temp);
            ref var spawnPoints = ref builder.ConstructRoot<EnemySpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, space.NumberOfSpawners);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var spawnPointOffset = new float3(0f, -2f, 1f);
            for (int i = 0; i < space.NumberOfSpawners; i++)
            {
                var newSpawner = ecb.Instantiate(space.SpawnerPrefab);
                var newSpawnerTransform = space.GetRandomSpaceTransform();
                ecb.SetComponent(newSpawner, new LocalTransform{Position = newSpawnerTransform.Position, Rotation = newSpawnerTransform.Rotation, Scale = newSpawnerTransform.Scale});
                
                var newEnemySpawnPoint = newSpawnerTransform.Position + spawnPointOffset;
                arrayBuilder[i] = newEnemySpawnPoint;
            }

            var blobAsset = builder.CreateBlobAssetReference<EnemySpawnPointsBlob>(Allocator.Persistent);
            ecb.SetComponent(spaceEntity, new EnemySpawnPoints
            {
                Value = blobAsset
            });
            builder.Dispose();
            
            ecb.Playback(state.EntityManager);
        }
    }
}