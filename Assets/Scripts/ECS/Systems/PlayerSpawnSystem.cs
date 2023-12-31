﻿using ECS.Components;
using ECS.Utils;
using ECS.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using Test.ObjectPooling;
using Test.Settings;
using Test.Views;
using UnityEngine;

namespace Test.Systems
{
    public class PlayerSpawnSystem : IEcsSystem, IEcsInitSystem
    {
        private EcsCustomInject<GameSettings> _gameSettings = default;
        private EcsCustomInject<SceneSettings> _sceneSettings = default;
        private EcsWorld _world;

        public static event Action<EntityView> PlayerSpawned;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            var playerEntity = _world.NewEntity();

            InstantiatePlayerView(playerEntity,
                _gameSettings.Value.PlayerPrefab,
                _sceneSettings.Value.playerSpawnPoint.transform.position,
                Quaternion.identity);
        }

        public void InstantiatePlayerView(int entity, PlayerView prefab, Vector3 position, Quaternion rotation)
        {
            var view = ObjectPoolManager.Spawn(prefab, position, rotation);
            view.Init(_world, entity);

            _world.AddComponent<ViewComponent>(entity).view = view;
            _world.AddComponent<TransformComponent>(entity).transform = view.transform;

            ref var player = ref _world.AddComponent<PlayerComponent>(entity);
            player.controller = view.CharacterController;

            PlayerSpawned?.Invoke(view);
        }
    }
}


