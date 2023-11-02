using System.Collections.Generic;
using Test.Settings;
using Test.Views;

namespace Test.Components
{
    public struct GeneratorComponent
    {
        public StackableHolderView view;

        public CollectableItemsConfig.CollectableObjectConfig stackableObjectConfig;

        public float spawnTimer;

        public Stack<int> producedEntities;
        public readonly int GeneratedCount => producedEntities.Count;
    }
}


