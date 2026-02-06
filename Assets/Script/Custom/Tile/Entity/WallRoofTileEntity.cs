using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Wall Tile", menuName = "Tile/Wall Roof Tile")]
public class WallRoofTileEntity : ScriptableObject
{
	public int id;

	public TileBase wallTile;

	public TileBase roofTile;

	public TileBase hiddenWallTile;

	public TileBase hiddenRoofTile;

	public TileBase overwriteGround;

	public TileBase overwriteUpperGround;

	public bool makeTransparentTileToAbove;
}
