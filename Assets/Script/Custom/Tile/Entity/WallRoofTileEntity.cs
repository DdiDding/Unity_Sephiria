using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tile/Entity/Wall Roof Tile")]
public class WallRoofTileEntity : TileEntityBase
{
	public TileBase wallTile;

	public TileBase roofTile;

	public TileBase hiddenWallTile;

	public TileBase hiddenRoofTile;

	public TileBase overwriteGround;

	public TileBase overwriteUpperGround;

	public bool makeTransparentTileToAbove;
}
