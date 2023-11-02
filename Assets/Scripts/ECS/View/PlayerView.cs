using ECS.Utils;
using ECS.View;
using Test.Components;
using UnityEngine;

namespace Test.Views
{
    public class PlayerView : EntityView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _itemsHolder;
        [SerializeField] private Animator _animator;

        public CharacterController CharacterController => _characterController;
        public Transform ItemsHolder => _itemsHolder;

        private void Update()
        {
            if (World == null)
                return;

            var moving = World.ReadComponent<MovementComponent>(Entity).direction != Vector3.zero;
            _animator.SetBool("IsMoving", moving);

            var hasItems = World.ReadComponent<PlayerItemsComponent>(Entity).Count > 0;
            _animator.SetBool("HasItems", hasItems);
        }
    }
}