using UnityEngine;
using UnityEngine.Tilemaps;
public enum TileGroupType
{
    Ground = 0,
    Wall,
    Water,
    MAX
}
public abstract class TileGroupBase : ScriptableObject
{
    public abstract System.Type GetTileType();
    public abstract void SetSize(int size);
    public abstract void Add(TileEntityBase tile);
    public abstract void Clear();

    public abstract TileBase GetTileBase(int index);
}
