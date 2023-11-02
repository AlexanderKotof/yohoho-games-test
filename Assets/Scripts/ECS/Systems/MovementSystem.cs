using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Settings;
using UnityEngine;

namespace Test.Systems
{
    public class MovementSystem : IEcsSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameSettings> _settings = default;

        private EcsFilterInject<Inc<PlayerComponent, MovementComponent>> _playerFilter = default;
        private EcsPoolInject<MovementComponent> _movementPool = default;
        private EcsPoolInject<PlayerComponent> _playerPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter.Value)
            {
                var movement = _movementPool.Value.Get(entity);
                var playerComponent = _playerPool.Value.Get(entity);

                MovePlayer(movement, playerComponent);
            }
        }

        private void MovePlayer(MovementComponent movement, PlayerComponent playerComponent)
        {
            playerComponent.controller.Move(movement.direction * _settings.Value.PlayerSpeed + Vector3.down * 0.1f);

            if (movement.direction != Vector3.zero)
                playerComponent.controller.transform.rotation = Quaternion.LookRotation(movement.direction, Vector3.up);
        }
    }
}


