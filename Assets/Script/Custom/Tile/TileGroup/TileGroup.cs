using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;


/**
 * @class TileEntityGroup
 * @briff 하나의 레이어의 TileEntity를 모아둔 List 관리
 * 타일을 인스펙터에서 설정해야하지만, 제너릭타입이므로 concrit 클래스를 따로 생성해두었음
 */
public class TileGroup<T> : TileGroupBase
    where T : TileEntityBase
{
    [SerializeField]
    protected List<T> tiles;

    public override System.Type GetTileType()
    {
        return typeof(T);
    }

    public override void SetSize(int size)
    {
        tiles.AddRange(Enumerable.Repeat<TileEntityBase>(null, size - tiles.Count));
    }

    public override void Add(TileEntityBase tile)
    {
        tiles.Add((T)tile);
    }

    public override void Clear()
    {
        tiles.Clear();
    }

    public T Get(int id)
    {
        return tiles[id];
    }

    public override TileBase GetTileBase(int index)
    {
        if (tiles.Count() <= index || tiles[index] == null)
        {
            return null;
        }

        return tiles[index].tile;
    }
}