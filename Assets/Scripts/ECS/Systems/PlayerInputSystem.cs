using ECS.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Test.Input;
using UnityEngine;

namespace Test.Systems
{
    public class PlayerInputSystem : IEcsSystem, IEcsRunSystem
    {
        private EcsCustomInject<IInputService> _input = default;

        private EcsFilterInject<Inc<PlayerComponent>> _playerFilter = default;
        private EcsPoolInject<MovementComponent> _movementPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach( var entity in _playerFilter.Value)
            {
                var input = _input.Value.GetInput();

                _movementPool.Value.GetOrAddComponent(entity).direction = ToXZ(_input.Value.GetInput());
            }
        }

        private Vector3 ToXZ(Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}


