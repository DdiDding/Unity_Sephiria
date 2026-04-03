using Game.Map;
using GameFramework;
using GameFramework.Resource;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class TileComponent : GameFrameworkComponent
{
    // --------------------------------------------
    // Private valebles
    // --------------------------------------------

    private Dictionary<ETileGroupType, TileGroupBase> tileGroups;
    private ResourceComponent resourceComponent;

    // --------------------------------------------
    // Life cycle
    // --------------------------------------------

    protected override void Awake()
    {
        base.Awake();
        tileGroups = new();
    }

    private void Start()
    {
        resourceComponent = GameEntry.GetComponent<ResourceComponent>();
    }

    // --------------------------------------------
    // Public functions
    // --------------------------------------------

    /**
     * @brief 타일 그룹을 로드한다.
     * @param OnTileLoadComplete 로드가 완료되면 콜백할 델리게이트용 함수
     */
    public void LoadTileGroup(Action OnTileLoadComplete)
    {
        resourceComponent = GameEntry.GetComponent<ResourceComponent>();
        if (resourceComponent == null)
        {
            Debug.LogError("ResourceComponent not found.");
            return;
        }

        
        // 모든 tile group이 들어있는 database 로드
        resourceComponent.LoadAsset("Assets/ScriptableObjects/Tiles/TileGroup/TileGroupDataBase.asset", typeof(TileGroupDataBase),
            new GameFramework.Resource.LoadAssetCallbacks(OnTileLoadedSuccess, OnTileLoadedFailure), OnTileLoadComplete
            );
    }

    /**
     * @brief 타일 타입에 맞는 타일 그룹을 반환한다.
     * @param tileType 얻으려는 타일 그룹의 타입
     */
    public TileGroupBase GetTileGroup(ETileGroupType tileType)
    {
        return tileGroups[tileType];
    }

    // --------------------------------------------
    // Private functions
    // --------------------------------------------

    /**
     * @brief LoadTileGroup의 성공 콜백 함수
     */
    private void OnTileLoadedSuccess(string assetName, object asset, float duration, object userData)
    {
        TileGroupDataBase tileGroupDB = asset as TileGroupDataBase;
        if (tileGroupDB == null)
        {
            Debug.LogError($"Asset '{assetName}' is null");
            return;
        }


        tileGroups.Add(ETileGroupType.Ground, tileGroupDB.groundTileGroup);
        tileGroups.Add(ETileGroupType.Wall, tileGroupDB.wallTileGroup);

        Action onLoadFinished = userData as Action;
        onLoadFinished?.Invoke();
    }

    /**
     * @brief LoadTileGroup의 실패 콜백 함수
     */
    private void OnTileLoadedFailure(string assetName, LoadResourceStatus status, string errorMessage, object userData)
    {
        Debug.Log("실패");
    }


}
