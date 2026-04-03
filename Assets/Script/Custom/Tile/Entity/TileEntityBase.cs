using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileEntityBase : ScriptableObject
{
    [SerializeField]
    private int id;
    public int Id => id;

    public TileBase tile;
}
