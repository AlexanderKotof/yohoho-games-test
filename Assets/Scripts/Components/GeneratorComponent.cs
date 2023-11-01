using System.Collections.Generic;
using Test.Settings;
using Test.Views;

namespace Test
{
    public struct GeneratorComponent
    {
        public StackableHolderView view;

        public CollectableItemsConfig.CollectableObjectConfig stackableObjectConfig;

        public float spawnTimer;

        public Stack<int> producedEntities;
        public int GeneratedCount => producedEntities.Count;
    }
}


