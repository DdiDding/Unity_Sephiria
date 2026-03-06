using UnityEngine;
using UnityEngine.Tilemaps;


// TODO : 상속을 ScriptableObject에서 TileEntityBase로 변경해야함.
// 리소스를 가져오면 변경하기
[CreateAssetMenu(fileName = "New Cliff Tile", menuName = "Tile/Cliff Tile")]
public class CliffTileEntity : ScriptableObject
{
	public int id;

	public TileBase tile;
}
