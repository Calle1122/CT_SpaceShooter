using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public readonly partial struct EnemyRiseAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<EnemyProperties.RiseRate> _enemyRiseRate;

        public void Rise(float deltaTime)
        {
            if (!IsAboveGround)
            {
                _localTransform.ValueRW.Position += math.up() * _enemyRiseRate.ValueRO.Value * deltaTime;
            }
        }

        public bool IsAboveGround => _localTransform.ValueRO.Position.y >= 0f;
    }
}