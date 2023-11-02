using Cinemachine;
using System;
using Test.Data;
using Test.FSM;
using Test.FSM.States;
using Test.ObjectPooling;
using Test.Settings;
using Test.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Test
{
    public class Bootstrap : LifetimeScope
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private CollectableItemsConfig _objectsConfig;
        [SerializeField] private SceneSettings _sceneSettings;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private UIManager _uiManager;

        private PlayerData _playerData;

        protected override void Configure(IContainerBuilder builder)
        {
            BindPlayerData(builder);
            BindSettings(builder);
            BindInput(builder);

            BindObjectsPool(builder);

            BindUI(builder);
            BindCamera(builder);

            BindStateMachine(builder);
        }

        private void BindObjectsPool(IContainerBuilder builder)
        {
            builder.Register<ObjectPoolManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindPlayerData(IContainerBuilder builder)
        {
            // TODO Load data
            _playerData = new PlayerData();

            builder.RegisterInstance(_playerData);
            builder.Register<PlayerDataHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindCamera(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            builder.Register<CameraInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(_uiManager);
        }

        private void BindSettings(IContainerBuilder builder)
        {
            builder.RegisterInstance(_settings);
            builder.RegisterInstance(_objectsConfig);
            builder.RegisterInstance(_sceneSettings);
        }

        private void BindInput(IContainerBuilder builder)
        {
            builder.Register<IInputService, KeyboardInput>(Lifetime.Singleton);
        }

        private void BindStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<PreGameState>(Lifetime.Singleton);
            builder.Register<GameState>(Lifetime.Singleton);

            builder.Register<GameStateMachineInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}