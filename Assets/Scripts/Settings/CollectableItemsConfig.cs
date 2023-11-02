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
            [SerializeField] private float _spawnTime;
            [SerializeField] private float _spendingTime;
            [SerializeField] private int _maxPlayerStack;
            [SerializeField] private int _maxGeneratorStack;
            [SerializeField] private int _maxCollectorStack;
            [SerializeField] private float _yOffset;

            public int Id => _id;
            public GameObject ObjectPrefab => _itemPrefab;
            public float SpawnTime => _spawnTime;
            public float SpendingTime => _spendingTime;
            public int MaxPlayerStack => _maxPlayerStack;
            public int MaxGeneratorStack => _maxGeneratorStack;
            public int MaxCollectorStack => _maxCollectorStack;
            public float YOffset => _yOffset;
        }

        [SerializeField] private CollectableObjectConfig[] _stackableObjects;
        [SerializeField] private float _interactionTime;

        public CollectableObjectConfig[] StackableObjects => _stackableObjects;
        public float InteractionTime => _interactionTime;


        public CollectableObjectConfig GetByIndex(int index)
        {
            return _stackableObjects[index];
        }
    }
}


