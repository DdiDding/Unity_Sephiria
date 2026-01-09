using GameFramework;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using UnityEngine;

public class TileMapData
{
    public Dictionary<int, RuleTile> ruleTiles = new Dictionary<int, RuleTile>();

    public void LoadTiles()
    {
        string path = "Assets/MonoBehaviour/GroundTile/0-Cave.asset";

        // ResourceComponent 가져오기
        ResourceComponent resource;
        {
            resource = GameEntry.GetComponent<ResourceComponent>();
            if (resource == null) return;
        }

        // LoadAsset의 콜백 정의
        LoadAssetCallbacks callbacks = new LoadAssetCallbacks(
            // 성공 콜백
            (assetName, asset, duration, userData) =>
            {
                RuleTile tile = asset as RuleTile;

                // null check
                if (tile == null)
                {
                    Debug.LogError($"Asset '{assetName}' is not TextAsset!");
                    return;
                }

                ruleTiles.Add(0, tile);
            },
            // 실패 콜백
            (assetName, status, errorMessage, userData) =>
            {
                Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
            }
        );

        resource.LoadAsset(path, callbacks);
        //Debug.Log($"");
    }

}
