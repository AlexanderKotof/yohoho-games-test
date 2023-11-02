using System;
using Test.Data;
using Test.Settings;
using Test.Systems;
using VContainer.Unity;

namespace Test
{
    public class PlayerDataHandler : IInitializable, IDisposable
    {
        private readonly PlayerData _data;

        public PlayerDataHandler(PlayerData data)
        {
            _data = data;
        }

        public void Dispose()
        {
            PlayerInteractionSystem.ItemReceivedByCollector -= OnItemReceivedByCollector;
        }

        public void Initialize()
        {
            PlayerInteractionSystem.ItemReceivedByCollector += OnItemReceivedByCollector;
        }

        private void OnItemReceivedByCollector(CollectableItemsConfig.CollectableObjectConfig itemConfig)
        {
            _data.AddCoins(itemConfig.CoinsReward);
        }
    }
}