using Unity.Entities;

namespace PGGDemo.ECS
{
    public struct SpawnInfo : IBufferElementData
    {
        public Entity Entity;
    }
}