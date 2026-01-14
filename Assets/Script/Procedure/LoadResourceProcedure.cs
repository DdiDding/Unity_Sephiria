using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class LoadResourceProcedure : ProcedureBase
{
    public TileProvider mapData = new TileProvider();
    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 가져올 테스트 경로
        string tempPath = "Assets/Resources/Rooms/Test_Moleland.txt";

        // 텍스트 맵 불러오기
        Game.Map.LoadTextMapData textMapLoader = new Game.Map.LoadTextMapData();
        Game.Map.TileMapInstaller tileMapInstaller = new Game.Map.TileMapInstaller();
        textMapLoader.LoadTextMap(tempPath, tileMapInstaller.OnMapLoaded);
    }
}