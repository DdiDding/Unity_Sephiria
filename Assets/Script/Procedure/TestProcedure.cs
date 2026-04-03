using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class TestProcedure : ProcedureBase
{
    // --------------------------------------------
    // Private valiables
    // --------------------------------------------
    private FloorComponent floorComponent;

    // --------------------------------------------
    // Life cycle
    // --------------------------------------------

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        floorComponent = GameEntry.GetComponent<FloorComponent>();

        // TODO : 후에 지금처럼 하나씩이 아닌 배열로 한 번에 room data받아와서, 한번에 방생성하기
        string testPath = "Assets/Resources/RoomDatas/Test_Moleland.txt";
        RoomDataParser.LoadTextFile(testPath, OnRoomDataLoaded);
    }

    // --------------------------------------------
    // Private functions
    // --------------------------------------------
    void OnRoomDataLoaded(RoomData roomData)
    {
        floorComponent.CreateRoom(roomData);
    }
}
