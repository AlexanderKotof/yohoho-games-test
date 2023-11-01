using ECS.Components;
using ECS.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Utils
{
    public static class EntityViewUtils
    {
        public static EntityView InstantiateView(this EcsWorld world, int entity, GameObject prefab)
        {
            return world.InstantiateView(entity, prefab, Vector3.zero, Quaternion.identity);
        }

        public static EntityView InstantiateView(this EcsWorld world, int entity, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var view = Object.Instantiate(prefab, position, rotation).AddComponent<EntityView>();
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
    }
}