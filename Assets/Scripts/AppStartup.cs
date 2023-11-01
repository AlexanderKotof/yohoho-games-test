using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Settings;
using UnityEngine;

namespace Test
{
    public class AppStartup : MonoBehaviour
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private StackableObjectsConfig _objectsConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Inject(_settings, _objectsConfig)
                .Init();
        }

        private void Update()
        {
            _systems.Run();
        }
    }
}