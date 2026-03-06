using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Ground Tile", menuName = "Tile/Ground Tile")]
public class GroundTileEntity : TileEntityBase
{
	public enum ELayer
	{
		Ground = 0,
		UpperGround = 1
	}

	public enum Type
	{
		Ground = 0,
		Pit = 1,
		Water = 2
	}

	public TileBase tile;

	public TileBase waterTile;

	[Header("Allowed layer")]
	public ELayer layer;

	public Type type;

	[Header("Pit Fall Plop")]
	public GameObject pitFallPlopFxPrefab;
}
