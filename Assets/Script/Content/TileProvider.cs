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
        private readonly Dictionary<int, TileBase> _tileCache = new Dictionary<int, TileBase>();
        private ResourceComponent _resource;

        private readonly Dictionary<int, string> _tilePathTable = new Dictionary<int, string>
        {
            { 0, "Assets/Resources/Tiles/0-Cave.asset" },
            { 2, "Assets/Resources/Tiles/2-Dirt.asset" },
            { 3, "Assets/Resources/Tiles/3-Stone.asset" },
        };


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

        public void LoadTiles( HashSet<int> tileIds, Action onAllLoaded)
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
            if (string.IsNullOrEmpty(path))
            {
                OnSingleTileLoaded();
                return;
            }

            _resource.LoadAsset(path, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    TileBase tile = asset as TileBase;
                    if (tile != null)
                    {
                        // 대체_tileCache[tileId] = tile;
                        _tileCache[0] = tile;
                    }
                    else
                    {
                        Debug.LogError($"Asset '{assetName}' is not TileBase.");
                    }

                    OnSingleTileLoaded();
                },
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
            //if (!_tilePathTable.TryGetValue(tileId, out string path))
            if (!_tilePathTable.TryGetValue(0, out string path))
            {
                Debug.LogError($"Tile path not found for tileId: {tileId}");
                return null;
            }

            return path;
        }
    }
}

//string path = "Assets/MonoBehaviour/GroundTile/0-Cave.asset";
//Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();