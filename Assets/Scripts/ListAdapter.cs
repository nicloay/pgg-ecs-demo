using System;
using JetBrains.Annotations;
using Unity.Collections;
using VContainer.Unity;

namespace PGGDemo
{
    [UsedImplicitly]
    public class ListAdapter : IStartable, IDisposable
    {
        public NativeList<EcsSpawnData> SpawnDatas;

        public void Dispose()
        {
            SpawnDatas.Dispose();
        }


        public void Start()
        {
            SpawnDatas = new NativeList<EcsSpawnData>(Allocator.Persistent);
        }
    }
}