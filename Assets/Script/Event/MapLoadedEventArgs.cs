using Game.Map.IO;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class MapLoadedEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(MapLoadedEventArgs).GetHashCode();
    public override int Id => EventId;
    
    public MapData mapData { get; private set; }

    public static MapLoadedEventArgs Create(MapData data)
    {
        MapLoadedEventArgs e = ReferencePool.Acquire<MapLoadedEventArgs>();
        e.mapData = data;
        return e;
    }

    public override void Clear()
    {
        mapData = default;
    }
}
