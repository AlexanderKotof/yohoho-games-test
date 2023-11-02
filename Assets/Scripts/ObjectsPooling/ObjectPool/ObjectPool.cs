﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test.ObjectPooling
{
    public class ObjectPool<T> : IDisposable
        where T : Component
    {
        private readonly List<T> _pool;
        private T _prefab;
        private Transform _parent;

        public ObjectPool(T prefab, Transform parentTransform, int prespawnCount = 1)
        {
            _prefab = prefab;
            _parent = parentTransform;

            _pool = new List<T>(prespawnCount);

            for (int i = 0; i < prespawnCount; i++)
            {
                Add().gameObject.SetActive(false);
            }
        }

        private T Add()
        {
            var obj = UnityEngine.Object.Instantiate(_prefab, _parent);
            _pool.Add(obj);
            return obj;
        }

        public T Pool()
        {
            foreach (var obj in _pool)
            {
                if (!obj.gameObject || obj.gameObject.activeSelf)
                    continue;

                obj.gameObject.SetActive(true);
                return obj;
            }

            return Add();
        }

        public void Dispose()
        {
            foreach (var obj in _pool)
            {
                UnityEngine.Object.Destroy(obj.gameObject);
            }

            _pool.Clear();
            _parent = null;
            _prefab = null;
        }
    }
}