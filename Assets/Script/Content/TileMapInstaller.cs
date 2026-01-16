using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    /**
     * @class TileMapInstaller
     * @brief MapData와 타일 리소스를 기반으로 실제 타일맵에 배치
     */
    public class TileMapInstaller
    {
        private MapData _mapData;
        private Action _onInstalled;

        private TileProvider _tileProvider;

        // 타일맵 참조 (외부에서 주입받는 것이 이상적)
        private Tilemap _groundMap;
        private Tilemap _upperGroundMap;
        private Tilemap _wallMap;
        private Tilemap _cliffMap;

        public TileMapInstaller()
        {
            _tileProvider = new TileProvider();
        }

        public void Install(
            MapData mapData,
            HashSet<int> neededTileIds,
            Action onInstalled)
        {
            _mapData = mapData;
            _onInstalled = onInstalled;

            // 타일맵 참조 획득 (예시는 Find, 실제로는 주입 권장)
            BindTilemaps();

            // 필요한 타일 요청
            _tileProvider.LoadTiles(
                neededTileIds,
                OnAllTilesLoaded
            );
        }

        private void BindTilemaps()
        {
            _groundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
            _upperGroundMap = GameObject.Find("UpperGround").GetComponent<Tilemap>();
            _wallMap = GameObject.Find("Wall").GetComponent<Tilemap>();
            _cliffMap = GameObject.Find("Cliff").GetComponent<Tilemap>();
        }

        // ----------------------------------
        // Tile Loaded Callback
        // ----------------------------------

        private void OnAllTilesLoaded()
        {
            InstallLayer(_mapData.ground, _groundMap);
            InstallLayer(_mapData.upperGround, _upperGroundMap);
            InstallLayer(_mapData.wall, _wallMap);
            InstallLayer(_mapData.cliff, _cliffMap);

            _onInstalled?.Invoke();
        }

        // ----------------------------------
        // Tile Placement
        // ----------------------------------

        private void InstallLayer(int[,] layerData, Tilemap tilemap)
        {
            if (layerData == null || tilemap == null)
                return;

            int height = layerData.GetLength(0);
            int width = layerData.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int tileId = layerData[y, x];
                    if (tileId < 0)
                        continue;

                    TileBase tile = _tileProvider.GetTile(tileId);
                    if (tile == null)
                        continue;

                    tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
    }
}
