using System;

namespace Test.Data
{
    public class PlayerData
    {
        public int Coins { get; private set; }

        public event Action CoinsChanged;

        public void AddCoins(int value)
        {
            Coins += value;
            CoinsChanged?.Invoke();
        }
    }
}