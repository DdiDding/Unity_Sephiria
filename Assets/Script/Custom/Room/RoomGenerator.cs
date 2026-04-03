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
    public RoomEntity GenerateRoom(RoomData roomData)
    {
        // TODO : 방이 여러개일때 이름은?
        GameObject roomObj = new GameObject("Room");
        RoomEntity room = roomObj.AddComponent<RoomEntity>();

        //TODO : ground와 upper은 합쳐도 됨
        setGroundTile(room.GetTilemap(ETileLayerType.Ground), roomData.ground);
        setUpperGroundTile(room.GetTilemap(ETileLayerType.UpperGround), roomData.upperGround);
        setWallRoofTile(room.GetTilemap(ETileLayerType.Wall), room.GetTilemap(ETileLayerType.Roof), roomData.wall);
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
    private bool setGroundTile(Tilemap tilemap, int[,] groundRoomData)
    {
        GroundTileGroup tileGroup = mTileComponent.GetTileGroup(ETileGroupType.Ground) as GroundTileGroup;
        int dataY = groundRoomData.GetLength(0);
        int dataX = groundRoomData.GetLength(1);


        // Ground Tile 생성
        {
            // 2차원 배열 room data 순회하면서 타일 설치
            for (int y = 0; y < dataY; ++y)
            {
                for (int x = 0; x < dataX; ++x)
                {
                    int tileNum = groundRoomData[y, x];
                    // 설치하지 않는 좌표일 때 = -1
                    if (tileNum == -1) continue;

                    TileBase tile = tileGroup.Get(tileNum).tile;

                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);
                    tilemap.SetTile(tilePosition, tile);
                }
            }
        }
        return true;
    }

    private bool setUpperGroundTile(Tilemap tilemap, int[,] upperGroundRoomData)
    {
        GroundTileGroup tileGroup = mTileComponent.GetTileGroup(ETileGroupType.Ground) as GroundTileGroup;
        int dataY = upperGroundRoomData.GetLength(0);
        int dataX = upperGroundRoomData.GetLength(1);

        {
            // 2차원 배열 room data 순회하면서 타일 설치
            for (int y = 0; y < dataY; ++y)
            {
                for (int x = 0; x < dataX; ++x)
                {
                    int tileNum = upperGroundRoomData[y, x];
                    // 설치하지 않는 좌표일 때 = -1
                    if (tileNum == -1) continue;

                    TileBase tile = tileGroup.Get(tileNum).tile;

                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);
                    tilemap.SetTile(tilePosition, tile);
                }
            }
        }
        return true;
    }

    private bool setWallRoofTile(Tilemap wallTilemap, Tilemap roofTilemap, int[,] wallRoomData)
    {
        WallTileGroup tileGroup = mTileComponent.GetTileGroup(ETileGroupType.Wall) as WallTileGroup;
        int dataY = wallRoomData.GetLength(0);
        int dataX = wallRoomData.GetLength(1);


        // Wall Tile 생성
        {
            // 2차원 배열 room data 순회하면서 타일 설치
            for (int y = 0; y < dataY; ++y)
            {
                for (int x = 0; x < dataX; ++x)
                {
                    int tileNum = wallRoomData[y, x];
                    // 설치하지 않는 좌표일 때 = -1
                    if (tileNum == -1) continue;

                    WallRoofTileEntity wallRoofTile = tileGroup.Get(tileNum);
                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);

                    // Wall & Roof tile 설치
                    wallTilemap.SetTile(tilePosition, wallRoofTile.wallTile);
                    roofTilemap.SetTile(tilePosition, wallRoofTile.roofTile);
                }
            }
        }

        wallTilemap.RefreshAllTiles();
        roofTilemap.RefreshAllTiles();
        return true;
    }

    // --------------------------------------------
    // Private values
    // --------------------------------------------
    // 타일맵 참조 (외부에서 주입받는 것이 이상적)
    private TileComponent mTileComponent;
}
