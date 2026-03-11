using GameFramework;
using GameFramework.Resource;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class TileComponent : GameFrameworkComponent
{

    private Dictionary<TileGroupType, TileGroupBase> tileGroups = new();
    ResourceComponent resourceComponent;

    // 타일 로드
    public void LoadTileGroup()
    {
        resourceComponent = GetComponent<ResourceComponent>();
        // Ground tile group 로드
        resourceComponent.LoadAsset("GroundTileGroup",
            new GameFramework.Resource.LoadAssetCallbacks(OnTileLoadedSuccess, OnTileLoadedFailure)
            );
    }

    private void OnTileLoadedSuccess(string assetName, object asset, float duration, object userData)
    {
        TileGroupBase tileGroup = asset as TileGroupBase;
        if (tileGroup != null)
        {
            // error check
        }
    }

    private void OnTileLoadedFailure(string assetName, LoadResourceStatus status, string errorMessage, object userData)
    {

    }

    // 타일 반환
    public TileBase TileEntityBase(TileGroupType tileType, int tileNum)
    {
        if (tileType == TileGroupType.Ground)
        {
            return tileGroups[TileGroupType.Ground].GetTileBase(tileNum);
        }

        return null;
    }
    
}
