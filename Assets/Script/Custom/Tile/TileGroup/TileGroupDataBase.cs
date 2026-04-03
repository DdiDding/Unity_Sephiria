using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Group/TileGroupDataBase")]
public class TileGroupDataBase : ScriptableObject
{
    public GroundTileGroup groundTileGroup;
    public WallTileGroup wallTileGroup;
}
