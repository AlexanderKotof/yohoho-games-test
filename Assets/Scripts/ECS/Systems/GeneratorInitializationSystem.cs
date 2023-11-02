using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Components;
using Test.Settings;
using Test.Views;
using UnityEngine;

namespace Test
{
    public class GeneratorInitializationSystem : IEcsSystem, IEcsInitSystem
    {
        private EcsCustomInject<SceneSettings> _sceneSettings = default;
        private EcsCustomInject<CollectableItemsConfig> _objectsConfig = default;

        private EcsPoolInject<GeneratorComponent> _generatorPool = default;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            var helpers = _sceneSettings.Value.generatorsSceneHelpers;

            foreach (var helper in helpers)
            {
                CreateGenerator(helper);
            }
        }

        private void CreateGenerator(AssignGeneratorView helper)
        {
            var entity = _world.Value.NewEntity();

            ref var generatorComponent = ref _generatorPool.Value.Add(entity);

            generatorComponent.view = helper.view;
            generatorComponent.stackableObjectConfig = _objectsConfig.Value.GetByIndex(helper.generateObjectId);
            generatorComponent.spawnTimer = Time.realtimeSinceStartup;

            generatorComponent.producedEntities = new System.Collections.Generic.Stack<int>();

            _world.Value.AssignView(entity, helper.view);
        }
    }
}


