using Game.Map;
using GameFramework.Procedure;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class LoadResourceProcedure : ProcedureBase
{
    private LoadTextMapData _mapLoader;
    private TileMapInstaller _installer;

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 흐름 시작 지점
        _mapLoader = new LoadTextMapData();
        _installer = new TileMapInstaller();

        // 예: 로드할 맵 경로
        string mapPath = "Assets/Resources/Rooms/Test_Moleland.txt";

        _mapLoader.LoadTextMap(
            mapPath,
            OnMapDataLoaded
        );
    }

    private void OnMapDataLoaded(
        MapData mapData,
        HashSet<int> neededTileIds)
    {
        // Procedure는 내용을 해석하지 않는다
        // 그냥 다음 작업자에게 넘긴다

        _installer.Install(
            mapData,
            neededTileIds,
            OnMapInstalled
        );
    }

    private void OnMapInstalled()
    {
        // 모든 타일 설치 완료
        // 다음 상태로 전환

        //ChangeState<EnterGameProcedure>();
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);

        _mapLoader = null;
        _installer = null;
    }
}
