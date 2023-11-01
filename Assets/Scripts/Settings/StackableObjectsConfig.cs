using System;
using UnityEngine;

namespace Test.Settings
{
    [CreateAssetMenu(menuName = "Settings/Stackable Objects Config")]
    public class StackableObjectsConfig : ScriptableObject
    {
        [Serializable]
        public class StackableObject
        {
            [SerializeField] private int _id;
            [SerializeField] private GameObject _objectPrefab;

            [SerializeField] private GameObject _collectorPrefab;

            [SerializeField] private int _maxStack;
            [SerializeField] private float _yOffset;

            public int Id => _id;
            public GameObject ObjectPrefab => _objectPrefab;
            public GameObject CollectorPrefab => _collectorPrefab;
            public int MaxStack => _maxStack;
            public float YOffset => _yOffset;
        }

        [SerializeField] private int _prespawnObjectsCount;
        [SerializeField] private float _spawnRate;
        [SerializeField] private StackableObject[] _stackableObjects;

        public int PrespawnObjectsCount => _prespawnObjectsCount;
        public float SpawnRate => _spawnRate;
        public StackableObject[] StackableObjects => _stackableObjects;
    }
}


