using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemySpawningSystem))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct EnemyRiseSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            new EnemyRiseJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct EnemyRiseJob : IJobEntity
    {

        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        
        [BurstCompile]
        private void Execute(EnemyRiseAspect enemy, [EntityIndexInQuery]int sortKey)
        {
            enemy.Rise(DeltaTime);
            if (!enemy.IsAboveGround) return;
            
            ECB.RemoveComponent<EnemyProperties.RiseRate>(sortKey, enemy.Entity);
        }
    }
}