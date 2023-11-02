using ECS.Components;
using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using Test.Components;
using Test.Data;
using Test.Settings;
using Test.Views;
using TMPro;
using UnityEngine;

namespace Test.Systems
{
    public class PlayerInteractionSystem : IEcsSystem, IEcsRunSystem
    {
        private EcsCustomInject<CollectableItemsConfig> _objectsConfig = default;
        private EcsCustomInject<GameSettings> _settings = default;

        private EcsFilterInject<Inc<PlayerComponent>> _playerFilter = default;
        private EcsFilterInject<Inc<GeneratorComponent>> _generatorsFilter = default;
        private EcsFilterInject<Inc<ItemsCollectorComponent>> _collectorsFilter = default;

        private EcsPoolInject<ViewComponent> _viewsPool = default;

        private EcsPoolInject<TransformComponent> _transformPool = default;
        private EcsPoolInject<PlayerItemsComponent> _collectedItemsPool = default;

        private EcsPoolInject<GeneratorComponent> _generatorPool = default;
        private EcsPoolInject<ItemsCollectorComponent> _collectorPool = default;


        public static event Action<CollectableItemsConfig.CollectableObjectConfig> ItemReceivedByCollector;


        public void Run(IEcsSystems systems)
        {
            foreach (var player in _playerFilter.Value)
            {
                ref var playerItems = ref _collectedItemsPool.Value.GetOrAddComponent(player);

                if (playerItems.lastInteractionTime + _objectsConfig.Value.InteractionTime > Time.realtimeSinceStartup)
                    continue;

                GeneratorsInteracton(player, ref playerItems);

                if (playerItems.Count <= 0)
                    continue;

                CollectorsInteraction(player, ref playerItems);
            }
        }
        private void GeneratorsInteracton(int player, ref PlayerItemsComponent collectedComponent)
        {
            foreach (var generator in _generatorsFilter.Value)
            {
                if (!DistanceCheck(player, generator, _settings.Value.InteractionDistance))
                    continue;

                ref var generatorComponent = ref _generatorPool.Value.Get(generator);

                if (generatorComponent.GeneratedCount <= 0)
                    continue;

                if (collectedComponent.Count > 0 && collectedComponent.itemId != -1 && collectedComponent.itemId != generatorComponent.stackableObjectConfig.Id)
                    continue;

                if (collectedComponent.Count >= generatorComponent.stackableObjectConfig.MaxPlayerStack)
                    continue;

                AddItemToPlayerStack(player, ref collectedComponent, ref generatorComponent);
            }
        }
        private void CollectorsInteraction(int player, ref PlayerItemsComponent playerItems)
        {
            foreach (var collector in _collectorsFilter.Value)
            {
                if (!DistanceCheck(player, collector, _settings.Value.InteractionDistance))
                    continue;

                ref var collectorComponent = ref _collectorPool.Value.Get(collector);

                if (collectorComponent.itemConfig.Id != playerItems.itemId)
                    continue;

                if (collectorComponent.Count >= collectorComponent.itemConfig.MaxCollectorStack)
                    continue;

                AddItemToCollectorFromPlayerStack(ref playerItems, ref collectorComponent);
            }
        }

        private void AddItemToCollectorFromPlayerStack(ref PlayerItemsComponent playerItems, ref ItemsCollectorComponent collectorComponent)
        {
            var itemEntity = playerItems.items.Pop();
            var itemView = _viewsPool.Value.Get(itemEntity).view;

            var targetPosition = Vector3.up * collectorComponent.itemConfig.YOffset * collectorComponent.Count;
            itemView.TweenView(collectorComponent.view.itemsHolderParent, targetPosition, Quaternion.identity);

            playerItems.lastInteractionTime = Time.realtimeSinceStartup;

            collectorComponent.spendTime = Time.realtimeSinceStartup + collectorComponent.itemConfig.SpendingTime;
            collectorComponent.items.Push(itemEntity);

            ItemReceivedByCollector?.Invoke(collectorComponent.itemConfig);
        }

        private void AddItemToPlayerStack(int player, ref PlayerItemsComponent collectedComponent, ref GeneratorComponent generatorComponent)
        {
            collectedComponent.itemId = generatorComponent.stackableObjectConfig.Id;

            var entity = generatorComponent.producedEntities.Pop();

            var view = _viewsPool.Value.Get(entity).view;
            var playerView = _viewsPool.Value.Get(player).view as PlayerView;

            var targetPosition = Vector3.up * _objectsConfig.Value.GetByIndex(collectedComponent.itemId).YOffset * collectedComponent.Count;

            view.TweenView(playerView.ItemsHolder, targetPosition, Quaternion.identity);

            collectedComponent.items.Push(entity);

            collectedComponent.lastInteractionTime = Time.realtimeSinceStartup;
        }

        private bool DistanceCheck(int entity, int otherEntity, float maxDistance)
        {
            return (_transformPool.Value.Get(entity).Position - _transformPool.Value.Get(otherEntity).Position).sqrMagnitude < maxDistance * maxDistance;
        }
    }
}


