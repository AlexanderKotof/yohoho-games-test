using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Settings;
using Test.Systems;
using UnityEngine;

namespace Test
{

    public class AppStartup : MonoBehaviour
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private CollectableObjectsConfig _objectsConfig;
        [SerializeField] private SceneSettings _sceneSettings;

        private IInputService _input;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            _input = new KeyboardInput();

            InitializeWorld();
        }

        private void InitializeWorld()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                // ������������ ���������� ������� �� �������� �� ���������� ������� ���������� ����:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                // ������������ ���������� ������� �� �������� �� ������� ������� ������. 
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Add(new PlayerSpawnSystem())
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                .Add(new GeneratorInitializationSystem())
                .Add(new GeneratorProductionSystem())
                .Inject(_settings, _objectsConfig, _sceneSettings, _input)
                .Init();
        }

        private void Update()
        {
            _systems.Run();
        }
    }
}