using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class RoomGenerator
{
    
    // room생성
    public void GenerateRoom(int[,] groundRoomData, int[,] upperGroundRoomData)
    {
        BindTilemaps();

        // tileComponent 가져오기
        if (tileComponent == null)
        {
            tileComponent = GameEntry.GetComponent<TileComponent>();
            if (tileComponent == null)
            {
                Debug.LogError("TileComponent not found.");
                return;
            }
        }

        // TODO : Test room data로 테스트 후 삭제 하기
        int[,] testGroundRoomData =
        {
            { 1, 1, 1, 1 },
            { 0, 1, 1, 0 },
            { 2, 3, 4, 1 }
        };

        GenerateGroundRoom(testGroundRoomData, testGroundRoomData);
    }

    /**
    * @brief 2차원배열대로 Ground, UpperGround Tile을 설치하는 함수
    */
    private bool GenerateGroundRoom(int[,] groundRoomData, int[,] upperGroundRoomData)
    {
        // Ground tile 생성
        Tilemap tileMap;
        GroundTileGroup tileGroup = tileComponent.GetTileGroup(TileGroupType.Ground) as GroundTileGroup;
        int y= groundRoomData.GetLength(0);
        int x = groundRoomData.GetLength(1);

        for (int i = 0; i < y; ++i)
        {
            for (int j = 0; j < x; ++x)
            {
                int tileId = groundRoomData[i, j];
                TileBase tile = tileGroup.Get(tileId).tile;


            }
        }

        // Upper Ground tile 생성
        return true;
    }

    private void BindTilemaps()
    {
        groundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
        upperGroundMap = GameObject.Find("UpperGround").GetComponent<Tilemap>();
        wallMap = GameObject.Find("Wall").GetComponent<Tilemap>();
        cliffMap = GameObject.Find("Cliff").GetComponent<Tilemap>();
    }

    // --------------------------------------------
    // Private values
    // --------------------------------------------
    // 타일맵 참조 (외부에서 주입받는 것이 이상적)
    private Tilemap groundMap;
    private Tilemap upperGroundMap;
    private Tilemap wallMap;
    private Tilemap cliffMap;
    private TileComponent tileComponent;
}
