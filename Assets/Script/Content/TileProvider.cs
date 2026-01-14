using GameFramework;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Collections;

/**
 * @class TileProvider
 * @brief 현재 사용되는 타일을 가지고 있으며, 타일을 요청할 시 제공해준다.
 * 만약 없다면 
 */
public class TileProvider
{
    public Dictionary<int, TileBase> ruleTiles = new Dictionary<int, TileBase>();
    public Tilemap tilemap;

    public void LoadTiles()
    {
        string path = "Assets/MonoBehaviour/GroundTile/0-Cave.asset";
        Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        // ResourceComponent 가져오기
        ResourceComponent resource;
        {
            resource = GameEntry.GetComponent<ResourceComponent>();
            if (resource == null) return;
        }

        resource.LoadAsset(path, new LoadAssetCallbacks(
            // 성공 콜백
            (assetName, asset, duration, userData) =>
            {
                TileBase tile = asset as TileBase;

                // null check
                if (tile == null)
                {
                    Debug.LogError($"Asset '{assetName}' is not TextAsset!");
                    return;
                }

                // 룰타일 Dictionary에 저장
                ruleTiles[0] = tile;

                // 테스트로 타일 깔아보기 -이건 삭제해도 됌
                tilemap.SetTile(new Vector3Int(0, 0, 0), tile);
            },
            // 실패 콜백
            (assetName, status, errorMessage, userData) =>
            {
                Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
            })
        );
    }
}
