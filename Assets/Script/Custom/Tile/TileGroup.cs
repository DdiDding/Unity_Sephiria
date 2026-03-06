using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * @class TileEntityGroup
 * @briff 각 TileEntity를 모아둔 Dictionary를 관리
 */
[CreateAssetMenu(fileName = "TileGroup", menuName = "Scriptable Objects/TileGroup")]
public class TileGroup<T> : ScriptableObject
    where T : TileEntityBase
{
    [SerializeField]
    private SerializedDictionary<int, T> tiles;
    public IReadOnlyDictionary<int, T> Tiles => tiles;

    public T Get(int id)
    {
        return tiles[id];
    }

    public bool TryGet(int id, out T entity)
    {
        return tiles.TryGetValue(id, out entity);
    }
}