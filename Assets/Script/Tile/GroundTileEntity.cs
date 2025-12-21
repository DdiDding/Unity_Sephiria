//using FMODUnity;
using UnityEngine;
using UnityEngine.Tilemaps;

// Asset형태의 데이터만 들고 있는 객체임 씬에 붙는게 아님
// 에셋메뉴에 해당 경로로 생성 가능하다는ㄷ?
[CreateAssetMenu(fileName = "New Ground Tile", menuName = "Tile/Ground Tile")]
public class GroundTileEntity : ScriptableObject
{
	// 레이어가 두 가지 존재함
	public enum ELayer
	{
		Ground = 0,
		UpperGround = 1
	}

	// 타일의 타입 3가지
	public enum Type
	{
		Ground = 0,
		Pit = 1, // 구멍, 낙사같은거
		Water = 2
	}

	public int id;

	public TileBase tile;

	public TileBase waterTile;

	[Header("Allowed layer")]
	public ELayer layer;

	public Type type;

	[Header("Pit Fall Plop")]
	public GameObject pitFallPlopFxPrefab;

	// FMOD의 이벤트 레퍼런스
	//public EventReference pitFallPlopSoundEvent;
}
