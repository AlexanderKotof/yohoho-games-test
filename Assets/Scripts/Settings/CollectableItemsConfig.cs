using System;
using UnityEngine;

namespace Test.Settings
{
    [CreateAssetMenu(menuName = "Settings/Stackable Objects Config")]
    public class CollectableItemsConfig : ScriptableObject
    {
        [Serializable]
        public class CollectableObjectConfig
        {
            [SerializeField] private int _id;
            [SerializeField] private GameObject _itemPrefab;
            [SerializeField] private float _spawnRate;
            [SerializeField] private float _spendingRate;
            [SerializeField] private int _maxPlayerStack;
            [SerializeField] private int _maxGeneratorStack;
            [SerializeField] private int _maxCollectorStack;
            [SerializeField] private float _yOffset;

            public int Id => _id;
            public GameObject ObjectPrefab => _itemPrefab;
            public float SpawnRate => _spawnRate;
            public float SpendingRate => _spendingRate;
            public int MaxPlayerStack => _maxPlayerStack;
            public int MaxGeneratorStack => _maxGeneratorStack;
            public int MaxCollectorStack => _maxCollectorStack;
            public float YOffset => _yOffset;
        }

        [SerializeField] private CollectableObjectConfig[] _stackableObjects;
        public CollectableObjectConfig[] StackableObjects => _stackableObjects;

        public CollectableObjectConfig GetByIndex(int index)
        {
            return _stackableObjects[index];
        }
    }
}


