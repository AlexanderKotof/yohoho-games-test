using UnityEngine;

public class EntityView : MonoBehaviour
{
    public int Entity { get; private set; }

    public void SetEntity(int entity)
    {
        Entity = entity;
    }
}
