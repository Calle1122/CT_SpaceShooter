using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct EnemySpawningSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new SpawnEnemyJob
            {
                DeltaTime = Time.deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }
    
    [BurstCompile]
    public partial struct SpawnEnemyJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        private void Execute(SpaceAspect space)
        {
            //Subtract enemy spawn timer and continue if timer reaches 0
            space.EnemySpawnTimer -= DeltaTime;
            if (!space.CanSpawnEnemy) return;

            //Reset enemy spawn timer
            space.EnemySpawnTimer = space.EnemySpawnRate;

            var newEnemy = ECB.Instantiate(space.EnemyPrefab);
        }
    }
}