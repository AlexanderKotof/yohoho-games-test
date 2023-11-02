using System;
using Test.Data;
using VContainer.Unity;

namespace Test.UI
{
    public class GameViewPresenter : IInitializable, IDisposable
    {
        private readonly PlayerData _data;
        private readonly UIManager _view;

        public GameViewPresenter(PlayerData data, UIManager view)
        {
            _data = data;
            _view = view;
        }

        public void Initialize()
        {
            _data.CoinsChanged += OnCoinsChanged;

            _view.SetCoins(_data.Coins);
        }
        public void Dispose()
        {
            _data.CoinsChanged -= OnCoinsChanged;
        }

        private void OnCoinsChanged()
        {
            _view.SetCoins(_data.Coins);
        }
    }
}