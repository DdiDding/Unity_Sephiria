using Game.Map;
using GameFramework;
using GameFramework.Resource;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class FloorComponent : GameFrameworkComponent
{
    // --------------------------------------------
    // Private valiables
    // --------------------------------------------
    private RoomGenerator roomGenerator;


    // --------------------------------------------
    // Life cycle
    // --------------------------------------------

    private void Start()
    {
        roomGenerator = new RoomGenerator(GameEntry.GetComponent<TileComponent>());
    }

    // --------------------------------------------
    // Public functions
    // --------------------------------------------
    public bool CreateRoom(RoomData roomData)
    {
        roomGenerator.GenerateRoom(roomData);
        return true;
    }

}
