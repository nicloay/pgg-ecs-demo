using FIMSpace.Generating;
using PGGDemo.ECS;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PGGDemo
{
    public class SceneScope : LifetimeScope
    {
        [SerializeField] private PrefabCollection prefabCollection;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(prefabCollection);
            builder.RegisterEntryPoint<ListAdapter>().AsSelf();
            builder.RegisterSystemFromDefaultWorld<SpawnSystem>();
            builder.RegisterComponentInHierarchy<BuildPlannerExecutor>();
        }
    }
}