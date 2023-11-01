using UnityEngine;

namespace Test.Settings
{
    [CreateAssetMenu(menuName = "Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Vector3 _playerSpawnPoint;
        [SerializeField] private Bounds _levelBounds;

        public GameObject PlayerPrefab => _playerPrefab;
        public Vector3 PlayerSpawnPoint => _playerSpawnPoint;
        public Bounds LevelBounds => _levelBounds;
    }
}


