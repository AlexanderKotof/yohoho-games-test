using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Input;
using Test.Settings;
using Test.Systems;
using Test.UI;

namespace Test.FSM.States
{
    public class GameState : IGameState, IStateUpdate
    {
        public IGameStateMachine FSM { get; private set; }

        private readonly GameSettings _settings;
        private readonly CollectableItemsConfig _objectsConfig;
        private readonly SceneSettings _sceneSettings;
        private readonly UIManager _uiManager;
        private readonly IInputService _input;

        private EcsWorld _world;
        private EcsSystems _systems;

        public GameState(IGameStateMachine fsm, GameSettings gameSettings, CollectableItemsConfig itemsConfig, SceneSettings sceneSettings, UIManager uiManager, IInputService input)
        {
            FSM = fsm;

            _settings = gameSettings;
            _objectsConfig = itemsConfig;
            _sceneSettings = sceneSettings;
            _uiManager = uiManager;
            _input = input;
        }

        public void Enter()
        {
            InitializeWorld();

            _uiManager.ShowInGameScreen();
        }

        private void InitializeWorld()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                // Регистрируем отладочные системы по контролю за текущей группой систем. 
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Add(new PlayerSpawnSystem())
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                .Add(new GeneratorInitializationSystem())
                .Add(new GeneratorProductionSystem())
                .Add(new CollectorsInitializationSystem())
                .Add(new CollectorsSpendingSystem())
                .Add(new PlayerInteractionSystem())
                .Inject(_settings, _objectsConfig, _sceneSettings, _input)
                .Init();
        }

        public void Exit()
        {
            _world.Destroy();
            _systems.Destroy();
        }

        public void Update()
        {
            _systems.Run();
        }
    }
}