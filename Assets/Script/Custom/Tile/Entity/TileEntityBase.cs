using UnityEngine;

public abstract class TileEntityBase : ScriptableObject
{
    [SerializeField]
    private int id;

    public int Id => id;
}
