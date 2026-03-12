using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class RoomGenerator
{
    // --------------------------------------------
    // Public functions
    // --------------------------------------------
    public RoomGenerator(TileComponent tileComponent)
    {
        mTileComponent = tileComponent;

        Debug.Assert(mTileComponent != null);
        if (mTileComponent == null)
        {
            Debug.LogError("TileComponent not found.");
        }
    }

    // room생성
    public RoomEntity GenerateRoom(int[,] groundRoomData, int[,] upperGroundRoomData)
    {
        // TODO : 룸 여러개일때 이름은?
        GameObject roomObj = new GameObject("Room");
        RoomEntity room = roomObj.AddComponent<RoomEntity>();

        setGroundTile(room.GetTilemap(ETileLayerType.Ground), groundRoomData, upperGroundRoomData);
        return room;
    }

    // --------------------------------------------
    // Private functions
    // --------------------------------------------

    /**
     * @brief 2차원배열대로 Ground, UpperGround Tile을 설치하는 함수
     * @param groundRoomData Ground레이어 타일 데이터
     * @param upperGroundRoomData Upper Ground레이어 타일 데이터
     * @return 타일 설치 성공 여부
     */
    private bool setGroundTile(Tilemap tilemap, int[,] groundRoomData, int[,] upperGroundRoomData)
    {
        GroundTileGroup tileGroup = mTileComponent.GetTileGroup(ETileGroupType.Ground) as GroundTileGroup;
        int dataY = groundRoomData.GetLength(0);
        int dataX = groundRoomData.GetLength(1);

        //TODO "X" 처리하기

        // Ground Tile 생성
        {
            // 2차원 배열 room data 순회하면서 타일 설치
            for (int y = 0; y < dataY; ++y)
            {
                for (int x = 0; x < dataX; ++x)
                {
                    int tileNum = groundRoomData[y, x];
                    TileBase tile = tileGroup.Get(tileNum).tile;

                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);
                    tilemap.SetTile(tilePosition, tile);
                }
            }
        }

        return true;
    }


    // --------------------------------------------
    // Private values
    // --------------------------------------------
    // 타일맵 참조 (외부에서 주입받는 것이 이상적)
    private TileComponent mTileComponent;
}
