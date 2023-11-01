using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Components;
using Test.Settings;
using Test.Views;

namespace Test
{
    public class CollectorsInitializationSystem : IEcsSystem, IEcsInitSystem
    {
        private EcsCustomInject<SceneSettings> _sceneSettings = default;
        private EcsCustomInject<CollectableItemsConfig> itemsConfig = default;

        private EcsPoolInject<ItemsCollectorComponent> _collectorPool = default;

        private EcsWorldInject _world;


        public void Init(IEcsSystems systems)
        {
            var helpers = _sceneSettings.Value.collectorsSceneHelpers;

            foreach (var helper in helpers)
            {
                CreateCollector(helper);
            }
        }

        private void CreateCollector(AssignCollectorView helper)
        {
            var entity = _world.Value.NewEntity();

            ref var collectorComponent = ref _collectorPool.Value.Add(entity);

            collectorComponent.view = helper.view;
            collectorComponent.itemConfig = itemsConfig.Value.GetByIndex(helper.collectObjectId);

            collectorComponent.spendTime = 0;
            collectorComponent.items = new System.Collections.Generic.Stack<int>();

            _world.Value.AssignView(entity, helper.view);
        }
    }
}


