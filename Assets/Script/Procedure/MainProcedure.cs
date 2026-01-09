using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class MainProcedure : ProcedureBase
{
    public TileMapData mapData = new TileMapData();
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 예: 초기 설정 로그 출력
        Log.Info("Game Start: ProcedureLaunch Entered.");
        //
        //멥 데이터 딕셔니를 저장
        mapData.LoadTiles();

        int a;
    }
}
