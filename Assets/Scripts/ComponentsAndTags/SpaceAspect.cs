using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace ComponentsAndTags
{
    public readonly partial struct SpaceAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;

        private readonly RefRO<SpaceProperties> _spaceProperties;
        private readonly RefRW<SpaceRandom> _spaceRandom;

        public int NumberOfSpawners => _spaceProperties.ValueRO.NumberOfSpawners;
        public Entity SpawnerPrefab => _spaceProperties.ValueRO.SpawnerPrefab;

        public LocalTransform GetRandomSpaceTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomSpacePosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(.75f)
            };
        }

        private float3 GetRandomSpacePosition()
        {
            float3 randomPosition;

            do
            {
                randomPosition = _spaceRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_localTransform.ValueRO.Position, randomPosition) <= BRAIN_SAFETY_RADIUS_SQ);

            return randomPosition;
        }

        private float3 MinCorner => _localTransform.ValueRO.Position - HalfDimensions;
        private float3 MaxCorner => _localTransform.ValueRO.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _spaceProperties.ValueRO.SpaceDimensions.x * 0.5f,
            y = 0f,
            z = _spaceProperties.ValueRO.SpaceDimensions.y * 0.5f
        };

        private const float BRAIN_SAFETY_RADIUS_SQ = 30f;

        private quaternion GetRandomRotation() => quaternion.RotateY(_spaceRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => _spaceRandom.ValueRW.Value.NextFloat(min, 1f);
    }
}