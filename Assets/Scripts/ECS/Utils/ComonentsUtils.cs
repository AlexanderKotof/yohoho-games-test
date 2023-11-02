using Leopotam.EcsLite;

namespace ECS.Utils
{
    public static class ComonentsUtils
    {
        public static ref T GetOrAddComponent<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
                return ref pool.Get(entity);

            return ref pool.Add(entity);
        }
        public static ref T AddComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            var pool = world.GetPool<T>();
            return ref pool.Add(entity);
        }

        public static T ReadComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            var pool = world.GetPool<T>();
            return pool.Get(entity);
        }

        public static ref T GetComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            var pool = world.GetPool<T>();
            return ref pool.Get(entity);
        }
    }
}