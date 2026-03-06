using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * @class TileGroup
 * @briff 각 TileEntity를 모아둔 Dictionary를 관리
 */
[CreateAssetMenu(fileName = "TileEntityGroup", menuName = "Scriptable Objects/TileEntityGroup")]
public class TileGroup : ScriptableObject
{
    public SerializedDictionary<int, GroundTileEntity> grounds = new SerializedDictionary<int, GroundTileEntity>();
    public IReadOnlyDictionary<int, GroundTileEntity> Grounds => grounds;

    public SerializedDictionary<int, char> test;

    [SerializeField]
    public SerializedDictionary<int, WallRoofTileEntity> walls;
    public IReadOnlyDictionary<int, WallRoofTileEntity> Walls => walls;

    [SerializeField]
    public SerializedDictionary<int, CliffTileEntity> cliffs;
    public IReadOnlyDictionary<int, CliffTileEntity> Cliffs=> cliffs;
}