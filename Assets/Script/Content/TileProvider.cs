using GameFramework.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace Game.Map
{
    /**
     * @class TileProvider
     * @brief 타일 리소스를 로드하고 캐싱하여 제공
     */
    public class TileProvider
    {
        private readonly Dictionary<int, TileBase> _tileCache =
            new Dictionary<int, TileBase>();

        private ResourceComponent _resource;

        // 비동기 로딩 상태 관리
        private int _loadingCount;
        private Action _onAllLoaded;

        public TileProvider()
        {
            _resource = GameEntry.GetComponent<ResourceComponent>();
            if (_resource == null)
            {
                Debug.LogError("ResourceComponent not found.");
            }
        }

        // ----------------------------------
        // Public API
        // ----------------------------------

        public void LoadTiles(
            HashSet<int> tileIds,
            Action onAllLoaded)
        {
            _onAllLoaded = onAllLoaded;

            _loadingCount = 0;

            foreach (int tileId in tileIds)
            {
                // 이미 로드된 타일이면 스킵
                if (_tileCache.ContainsKey(tileId))
                    continue;

                _loadingCount++;
                LoadTileAsync(tileId);
            }

            // 로드할 게 하나도 없는 경우
            if (_loadingCount == 0)
            {
                _onAllLoaded?.Invoke();
            }
        }

        public TileBase GetTile(int tileId)
        {
            _tileCache.TryGetValue(tileId, out TileBase tile);
            return tile;
        }

        // ----------------------------------
        // Internal
        // ----------------------------------

        private void LoadTileAsync(int tileId)
        {
            string path = GetTilePath(tileId);

            _resource.LoadAsset(path, new LoadAssetCallbacks(
                // 성공
                (assetName, asset, duration, userData) =>
                {
                    TileBase tile = asset as TileBase;
                    if (tile == null)
                    {
                        Debug.LogError($"Asset '{assetName}' is not TileBase.");
                    }
                    else
                    {
                        _tileCache[tileId] = tile;
                    }

                    OnSingleTileLoaded();
                },
                // 실패
                (assetName, status, errorMessage, userData) =>
                {
                    Debug.LogError($"Failed to load tile '{assetName}': {errorMessage}");
                    OnSingleTileLoaded();
                }
            ));
        }

        private void OnSingleTileLoaded()
        {
            _loadingCount--;

            if (_loadingCount <= 0)
            {
                _onAllLoaded?.Invoke();
            }
        }

        private string GetTilePath(int tileId)
        {
            // 규칙은 여기서만 안다
            // 예: Assets/Resources/Tiles/Tile_12.asset
            return $"Assets/Resources/Tiles/Tile_{tileId}.asset";
        }
    }
}

//string path = "Assets/MonoBehaviour/GroundTile/0-Cave.asset";
//Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();