using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.View
{
    public class EntityView : MonoBehaviour
    {
        public EcsWorld World { get; private set; }
        public int Entity { get; private set; }

        public void Init(EcsWorld world, int entity)
        {
            Entity = entity;
            World = world;
        }
    }
}