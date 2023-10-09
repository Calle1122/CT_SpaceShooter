using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnEnemySystem : ISystem
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

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            for (int i = 0; i < space.NumberOfSpawners; i++)
            {
                var newSpawner = ecb.Instantiate(space.SpawnerPrefab);
                var newSpawnerTransform = space.GetRandomSpaceTransform();
                ecb.SetComponent(newSpawner, new LocalTransform{Position = newSpawnerTransform.Position, Rotation = newSpawnerTransform.Rotation, Scale = newSpawnerTransform.Scale});
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}