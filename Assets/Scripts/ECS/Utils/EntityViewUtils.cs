using DG.Tweening;
using ECS.Components;
using ECS.View;
using Leopotam.EcsLite;
using Test.ObjectPooling;
using UnityEngine;

namespace ECS.Utils
{
    public static class EntityViewUtils
    {
        public static EntityView InstantiateView(this EcsWorld world, int entity, EntityView prefab)
        {
            return world.InstantiateView(entity, prefab, Vector3.zero, Quaternion.identity);
        }

        public static EntityView InstantiateView(this EcsWorld world, int entity, EntityView prefab, Vector3 position, Quaternion rotation)
        {
            var view = ObjectPoolManager.Spawn(prefab, position, rotation);
            view.Init(world, entity);

            world.AddComponent<ViewComponent>(entity).view = view;
            world.AddComponent<TransformComponent>(entity).transform = view.transform;

            return view;
        }

        public static void AssignView(this EcsWorld world, int entity, EntityView view)
        {
            view.Init(world, entity);

            world.AddComponent<ViewComponent>(entity).view = view;
            world.AddComponent<TransformComponent>(entity).transform = view.transform;
        }

        public static void TweenView(this EntityView view, Transform parentTransform, Vector3 targetPosition, Quaternion targetRotation)
        {
            var viewTransform = view.transform;

            var sequence = DOTween.Sequence();
            sequence.Append(viewTransform.DOMove(viewTransform.position + Vector3.up * 2 + Random.onUnitSphere, 0.1f)).SetEase(Ease.InExpo);
            sequence.AppendCallback(() => viewTransform.SetParent(parentTransform));
            sequence.Append(viewTransform.DOLocalRotateQuaternion(targetRotation, 0.3f)).SetEase(Ease.InExpo);
            sequence.Append(viewTransform.DOLocalMove(targetPosition, 0.5f)).SetEase(Ease.InExpo);
            sequence.Play();
        }
    }
}