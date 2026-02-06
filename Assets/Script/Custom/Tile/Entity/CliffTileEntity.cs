using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Cliff Tile", menuName = "Tile/Cliff Tile")]
public class CliffTileEntity : ScriptableObject
{
	public int id;

	public TileBase tile;
}
