using System.Collections.Generic;
using Test.Settings;
using Test.Views;

namespace Test.Components
{
    public struct ItemsCollectorComponent
    {
        public StackableHolderView view;

        public CollectableItemsConfig.CollectableObjectConfig itemConfig;
        public int Count => items.Count;

        public Stack<int> items;

        public float spendTime;
    }
}
