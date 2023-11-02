using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Components;
using Test.ObjectPooling;
using UnityEngine;

namespace Test.Systems
{
    public class CollectorsSpendingSystem : IEcsSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<ItemsCollectorComponent>> _collectorsFilter = default;
        private EcsPoolInject<ItemsCollectorComponent> _collectorsPool = default;

        private EcsPoolInject<ViewComponent> _viewPool = default;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _collectorsFilter.Value)
            {
                ref var collectorComponent = ref _collectorsPool.Value.Get(entity);

                if (collectorComponent.Count <= 0)
                    continue;

                if (collectorComponent.spendTime > Time.realtimeSinceStartup)
                    continue;

                collectorComponent.spendTime = Time.realtimeSinceStartup + collectorComponent.itemConfig.SpendingTime;
               
                var itemEntity = collectorComponent.items.Pop();

                var view = _viewPool.Value.Get(itemEntity).view;

                ObjectPoolManager.Despawn(view);

                _world.Value.DelEntity(itemEntity);
            }
        }
    }
}


