using Unity.Entities;
using UnityEngine;

namespace PGGDemo.ECS
{
    public class SpawnAuthoring : MonoBehaviour
    {
        [SerializeField] private PrefabCollection prefabCollection;

        public class SpawnInfoBaker : Baker<SpawnAuthoring>
        {
            public override void Bake(SpawnAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var buffer = AddBuffer<SpawnInfo>(entity);
                foreach (var prefab in authoring.prefabCollection.Prefabs)
                    buffer.Add(new SpawnInfo { Entity = GetEntity(prefab, TransformUsageFlags.Dynamic) });
            }
        }
    }
}