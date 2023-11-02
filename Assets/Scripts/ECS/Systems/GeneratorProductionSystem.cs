using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Components;
using UnityEngine;

namespace Test.Systems
{
    public class GeneratorProductionSystem : IEcsSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<GeneratorComponent>> _generatorsFilter = default;
        private EcsPoolInject<GeneratorComponent> _generatorPool = default;

        private EcsWorldInject _world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _generatorsFilter.Value)
            {
                ref var generator = ref _generatorPool.Value.Get(entity);

                if (Time.realtimeSinceStartup < generator.spawnTimer + generator.stackableObjectConfig.SpawnTime)
                    continue;

                if (generator.GeneratedCount >= generator.stackableObjectConfig.MaxGeneratorStack)
                    continue;

                GenerateObject(ref generator);
            }
        }

        private void GenerateObject(ref GeneratorComponent generator)
        {
            var stackableEntity = _world.Value.NewEntity();

            var position = generator.view.itemsHolderParent.position + Vector3.up * generator.stackableObjectConfig.YOffset * generator.GeneratedCount;

            _world.Value.InstantiateView(stackableEntity, generator.stackableObjectConfig.ObjectPrefab, position, Quaternion.identity);

            ref var stackable = ref _world.Value.AddComponent<StackableObjectComponent>(stackableEntity);
            stackable.id = generator.stackableObjectConfig.Id;

            generator.spawnTimer = Time.realtimeSinceStartup;
            generator.producedEntities.Push(stackableEntity);
        }
    }
}


