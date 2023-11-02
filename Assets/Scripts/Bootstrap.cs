using Test.FSM;
using Test.FSM.States;
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
        [SerializeField] private UIManager _uiManager;

        protected override void Configure(IContainerBuilder builder)
        {
            BindSettings(builder);

            BindInput(builder);

            InitStateMachine(builder);
        }

        private void BindSettings(IContainerBuilder builder)
        {
            builder.RegisterInstance(_settings);
            builder.RegisterInstance(_objectsConfig);
            builder.RegisterInstance(_sceneSettings);
            builder.RegisterInstance(_uiManager);
        }

        private void BindInput(IContainerBuilder builder)
        {
            builder.Register<IInputService, KeyboardInput>(Lifetime.Singleton);
        }

        private void InitStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<PreGameState>(Lifetime.Singleton);
            builder.Register<GameState>(Lifetime.Singleton);

            builder.Register<GameStateMachineInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}