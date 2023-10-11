using Unity.Entities;

namespace ComponentsAndTags
{
    public static class EnemyProperties
    {
        public struct Walk : IComponentData
        {
            public float WalkSpeed;
            public float WalkAmplitude;
            public float WalkFrequency;
        }
        
        public struct Timer : IComponentData
        {
            public float Value;
        }
        
        public struct RiseRate : IComponentData
        {
            public float Value;
        }
    }
}