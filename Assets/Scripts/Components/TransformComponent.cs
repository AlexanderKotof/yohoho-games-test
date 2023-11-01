using UnityEngine;

namespace ECS.Components
{
    public struct TransformComponent
    {
        public Transform transform;
        public Vector3 Position => transform.position;
    }
}