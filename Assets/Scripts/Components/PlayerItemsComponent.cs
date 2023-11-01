using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Test.Components
{
    public struct PlayerItemsComponent : IEcsAutoReset<PlayerItemsComponent>
    {
        public int itemId;
        public Stack<int> items;
        public readonly int Count => items.Count;

        public void AutoReset(ref PlayerItemsComponent c)
        {
            c.itemId = -1;
            c.items = new Stack<int>();
        }
    }
}


