using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Components;
using Test.Settings;
using UnityEngine;

namespace Test
{
    public class StackableObjectsSpawnSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameSettings> _gameSettings = default;
        private EcsCustomInject<StackableObjectsConfig> _objectsConfig = default;

        private EcsWorld _world;
        private int _timerEntity;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            InitializeTimer();

            for (int i = 0; i < _objectsConfig.Value.PrespawnObjectsCount; i++)
            {
                SpawnRandomObject();
            }
        }

        private void InitializeTimer()
        {
            _timerEntity = _world.NewEntity();
            _world.AddComponent<SpawnerTimerComponent>(_timerEntity);
        }

        private void SpawnRandomObject()
        {
            var objectsConfig = _objectsConfig.Value;
            var config = objectsConfig.StackableObjects[Random.Range(0, objectsConfig.StackableObjects.Length)];

            SpawnObject(config);
        }

        private void SpawnObject(StackableObjectsConfig.StackableObject objectConfigToSpawn)
        {
            var entity = _world.NewEntity();

            var bounds = _gameSettings.Value.LevelBounds;
            var position = new Vector3(bounds.min.x + Random.value * bounds.size.x, 0, bounds.min.z + Random.value * bounds.size.z);

            _world.InstantiateView(entity, objectConfigToSpawn.ObjectPrefab, position, Quaternion.identity);

            ref var stackable = ref _world.AddComponent<StackableObjectComponent>(entity);
            stackable.id = objectConfigToSpawn.Id;
        }

        public void Run(IEcsSystems systems)
        {
            var time = Time.timeSinceLevelLoad;

            ref var lastSpawnTime = ref _world.GetComponent<SpawnerTimerComponent>(_timerEntity);

            if (time >= lastSpawnTime.timer + _objectsConfig.Value.SpawnRate)
            {
                lastSpawnTime.timer = time;
                SpawnRandomObject();
            }
        }
    }
}


