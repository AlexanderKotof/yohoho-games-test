using Test.Views;
using UnityEngine;

namespace Test.Settings
{
    [CreateAssetMenu(menuName = "Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private PlayerView _playerPrefab;

        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _acceleration;

        [SerializeField] private float _interactionDistance;

        [SerializeField] private Bounds _levelBounds;

        public PlayerView PlayerPrefab => _playerPrefab;
        public float PlayerSpeed => _playerSpeed;
        public float Acceleration => _acceleration;
        public float InteractionDistance => _interactionDistance;
        public Bounds LevelBounds => _levelBounds;
    }
}


