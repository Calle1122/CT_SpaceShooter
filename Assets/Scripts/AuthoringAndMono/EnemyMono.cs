using ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class EnemyMono : MonoBehaviour
    {
        public float RiseRate;
    }

    public class EnemyBaker : Baker<EnemyMono>
    {
        public override void Bake(EnemyMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyProperties.RiseRate { Value = authoring.RiseRate });
        }
    }
}