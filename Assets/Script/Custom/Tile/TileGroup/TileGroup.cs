using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

enum TileGroupType
{
    Ground = 0,
    Wall,
    Water,
    MAX
}

/**
 * @class TileEntityGroup
 * @briff 하나의 레이어의 TileEntity를 모아둔 List 관리
 * 타일을 인스펙터에서 설정해야하지만, 제너릭타입이므로 concrit 클래스를 따로 생성해두었음
 */
public class TileGroup<T> : ScriptableObject
    where T : TileEntityBase
{
    [SerializeField]
    private T[] tiles;
   
    public T Get(int id)
    {
        return tiles[id];
    }
}