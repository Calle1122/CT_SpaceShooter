using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct SpaceRandom : IComponentData
    {
        public Random Value;
    }
}