using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Test.ObjectPooling
{
    public class ObjectPoolManager : IInitializable, IDisposable
    {
        private static ObjectPoolManager _instance;

        private readonly Dictionary<Component, ObjectPool<Component>> _componentToObjectPools = new Dictionary<Component, ObjectPool<Component>>();

        private Transform _parent;

        public void Initialize()
        {
            _instance = this;
            _parent = new GameObject("ObjectsPool").transform;
        }

        public void Dispose()
        {
            foreach (var pool in _componentToObjectPools.Values)
            {
                pool.Dispose();
            }
            _componentToObjectPools.Clear();
            _instance = null;
        }

        private ObjectPool<Component> RegisterPrefab(Component prefab)
        {
            if (_componentToObjectPools.TryGetValue(prefab, out var pool))
            {
                return pool;
            }

            Debug.Log($"Registered new prefab {prefab.gameObject.name} of type {prefab.GetType().Name}");

            pool = new ObjectPool<Component>(prefab, _parent);
            _componentToObjectPools.Add(prefab, pool);

            return pool;
        }

        public static Component Spawn(Component prefab, Vector3 position, Quaternion rotation)
        {
            var pool = _instance.RegisterPrefab(prefab);
            var obj = pool.Pool();

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public static Component Spawn(Component prefab)
        {
            return Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        public static T Spawn<T>(T component, Vector3 position, Quaternion rotation) where T : Component
        {
            return Spawn((Component)component, position, rotation) as T;
        }

        public static T Spawn<T>(T component) where T : Component
        {
            return Spawn((Component)component) as T;
        }

        public static void Despawn(Component instance)
        {
            instance.gameObject.SetActive(false);
        }
    }
}