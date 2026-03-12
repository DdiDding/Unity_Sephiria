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
    public bool CreateRoom()
    {

        // TODO : Test room data로 테스트 후 삭제 하기
        int[,] testGroundRoomData =
        {
            { 1, 1, 1, 1 },
            { 0, 1, 1, 0 },
            { 2, 3, 4, 1 }
        };

        int[,] testUpperGroundRoomData =
        {
            { 0, 0, 0, 0},
            { 0, 8, 9, 0 },
            { 0, 0, 0, 0}
        };

        roomGenerator.GenerateRoom(testGroundRoomData, testUpperGroundRoomData);

        return true;
    }

}
