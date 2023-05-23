using JetBrains.Annotations;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using VContainer;

namespace PGGDemo.ECS
{
    public partial class SpawnSystem : SystemBase
    {
        private ListAdapter _listAdapter;

        [Inject]
        [UsedImplicitly]
        public void Inject(ListAdapter listAdapter)
        {
            _listAdapter = listAdapter;
        }

        protected override void OnCreate()
        {
            RequireForUpdate<SpawnInfo>();
        }

        protected override void OnUpdate()
        {
            if (_listAdapter.SpawnDatas.IsEmpty) return;


            var spawnInfos = SystemAPI.GetSingletonBuffer<SpawnInfo>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (_, entity) in SystemAPI.Query<LocalTransform>().WithEntityAccess().WithNone<SpawnInfo>())
                ecb.DestroyEntity(entity);

            foreach (var info in _listAdapter.SpawnDatas)
            {
                var entity = ecb.Instantiate(spawnInfos[info.PrefabId].Entity);
                ecb.AddComponent(entity, new LocalTransform
                {
                    Position = info.Position,
                    Rotation = info.Rotation,
                    Scale = info.UniformScale
                });
                ecb.AddComponent<StaticOptimizeEntity>(entity);
            }

            ecb.Playback(EntityManager);

            _listAdapter.SpawnDatas.Clear();
        }
    }
}