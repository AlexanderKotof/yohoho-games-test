using ECS.Utils;
using ECS.View;
using Test.Components;
using UnityEngine;

namespace Test.Views
{
    public class PlayerView : EntityView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _itemsHolder;
        [SerializeField] private Animator _animator;

        public Rigidbody Rigidbody => _rigidbody;
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