using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * @class TileEntityGroup
 * @briff 각 TileEntity를 모아둔 Dictionary를 관리
 */
[CreateAssetMenu(fileName = "TileGroup", menuName = "Scriptable Objects/TileGroup")]
public class TileGroup : ScriptableObject
{
    [SerializeField]
    public SerializedDictionary<int, GroundTileEntity> grounds = new SerializedDictionary<int, GroundTileEntity>();
    public IReadOnlyDictionary<int, GroundTileEntity> Grounds => grounds;

    [SerializeField]
    public SerializedDictionary<int, WallRoofTileEntity> walls;
    public IReadOnlyDictionary<int, WallRoofTileEntity> Walls => walls;

    [SerializeField]
    public SerializedDictionary<int, CliffTileEntity> cliffs;
    public IReadOnlyDictionary<int, CliffTileEntity> Cliffs=> cliffs;
}