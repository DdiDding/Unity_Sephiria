using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEntity : MonoBehaviour
{
    // --------------------------------------------
    // Private Variables
    // --------------------------------------------

    // 0: Ground, 1: UpperGround, 2: Wall, 3: Cliff
    private Tilemap[] tilemaps;
    private Grid grid;

    // --------------------------------------------
    // Life cycle
    // --------------------------------------------
    private void Awake()
    {
        // Grid 생성
        {
            GameObject gridGO = new GameObject("Grid");
            grid = gridGO.AddComponent<Grid>();
        }

        // 타일 맵의 레이어 순서 설정
        {
            tilemaps = new Tilemap[(int)ETileLayerType.MAX];

            tilemaps[(int)ETileLayerType.Ground] = createTilemap("Ground", -100);
            tilemaps[(int)ETileLayerType.UpperGround] = createTilemap("UpperGround", -99);
            tilemaps[(int)ETileLayerType.Cliff] = createTilemap("Cliff", -98);
            tilemaps[(int)ETileLayerType.Wall] = createTilemap("Wall", -97);
            tilemaps[(int)ETileLayerType.Roof] = createTilemap("Roof", -96);
        }
    }
    private void Start()
    {
        // TODO : RoomEntity 초기화 작업
    }

    // --------------------------------------------
    // Public functions
    // --------------------------------------------
    public Tilemap GetTilemap(ETileLayerType layer)
    {
        int index = (int)layer;
        if (index < 0 || index >= tilemaps.Length)
        {
            Debug.LogError("tilemap index out of range: " + index);
            return null;
        }
        return tilemaps[index];
    }

    // --------------------------------------------
    // Private functions
    // --------------------------------------------
    private Tilemap createTilemap(string name, int sortingOrder)
    {
        GameObject tilemapObj = new GameObject(name);
        tilemapObj.transform.parent = grid.transform;

        Tilemap tilemap = tilemapObj.AddComponent<Tilemap>();
        TilemapRenderer renderer = tilemapObj.AddComponent<TilemapRenderer>();
        renderer.sortingOrder = sortingOrder;
        
        return tilemap;
    }
}
