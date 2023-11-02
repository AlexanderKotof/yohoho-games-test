using Cinemachine;
using ECS.View;
using System;
using Test.Systems;
using VContainer.Unity;

namespace Test
{
    public class CameraInitializer : IInitializable, IDisposable
    {
        private readonly CinemachineVirtualCamera _camera;

        public CameraInitializer(CinemachineVirtualCamera camera)
        {
            _camera = camera;
        }

        public void Initialize()
        {
            PlayerSpawnSystem.PlayerSpawned += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(EntityView view)
        {
            _camera.Follow = view.transform;
        }

        public void Dispose()
        {
            PlayerSpawnSystem.PlayerSpawned -= OnPlayerSpawned;
        }
    }
}